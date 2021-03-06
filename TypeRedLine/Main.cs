﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Shared;
using TypeRacingDao.Entities;
using TypeRedLine.Properties;
using TypeRacingDao;

namespace TypeRedLine
{
    public partial class Main : Form
    {
        private readonly Point typingBoxStartLocation = new Point(0, 20);
        private const string BestWpmFormat = "Best Score: {0:f2}";
        private const string WpmFormat = "WPM: {0:f2}";

        private string currentText;
        private int currentTextIndex;
        private List<string> words;
        private string currentWord;
        private int currentIndex;
        private Font defaultFont;
        private bool inProgress;

        private int keyStrokeCount;
        private int mistakeCount;
        private int previousLength;




        private int currentHighlightStart;
        private int currentHighlightLength;
        private int currentLine;

        private List<Race> races;
        private List<Record> records;
        

        private IList<Player> players;

        private Race currentRace;
        private Player currentPlayer;

        private Random gen;
        private DateTime? start;
        private DateTime? finish;

        private List<double> bestRates;
        private double cpm;
        private double wpm;
        


        public Main()
        {
            InitializeComponent();
            
            typingBoxStartLocation = new Point(0, 20);
            keyStrokeCount = 0;
            mistakeCount = 0;

            races = RaceDao.GetAll().ToList();
            records = new List<Record>();
            foreach (var race in races)
            {
                records.AddRange(race.Records);
            }
            players = PlayerDao.GetAll();

            comboBox1.DataSource = players;
            comboBox1.DisplayMember = "Name";
            currentPlayer = players[0];


            gen = new Random();

            defaultFont = rtbRaceText.Font;

            bestRates = new List<double>();

            foreach (var t in races)
            {
                if (t.Records.Count > 0)
                {
                    bestRates.Add(t.Records.Max(rec => rec.CPM));
                }
                else
                {
                    bestRates.Add(0.0);
                }
            }


            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.ForeColor = Color.Green;

            ResetRace();
            inProgress = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetRace();
        }

        private void ResetRace()
        {
            keyStrokeCount = 0;
            mistakeCount = 0;

            txtTypingBox.Location = typingBoxStartLocation;
            txtTypingBox.Focus();


            lblCurrentWpm.Visible = true;
            lblBestWpm.Visible = true;

            currentTextIndex = gen.Next(races.Count);
            lblCurrentWpm.Text = Resources.WPMString;
            
            //lblBestWpm.Text = string.Format(BestWpmFormat, bestRates[currentTextIndex]);

            currentRace = races[currentTextIndex];
            currentText = currentRace.Text;
            
            char[] space = { ' ' };
            words = new List<string>(currentText.Split(space));

            UpdateBestWpm();

            rtbRaceText.Text = currentText;


            currentLine = 0;
            currentIndex = 0;
            currentWord = words[currentIndex] ?? string.Empty;

            currentHighlightStart = 0;
            currentHighlightLength = currentWord.Length;

            txtTypingBox.Text = string.Empty;
            start = null;

            progressBar.Value = 0;

            rtbRaceText.Select(0, rtbRaceText.Text.Length);
            rtbRaceText.SelectionBackColor = Color.White;
            rtbRaceText.SelectionColor = Color.Black;
            rtbRaceText.SelectionFont = defaultFont;

            rtbRaceText.Select(currentHighlightStart, currentHighlightLength);
            rtbRaceText.SelectionColor = Color.Blue;
            rtbRaceText.SelectionFont = new Font(rtbRaceText.SelectionFont, FontStyle.Underline);

            txtTypingBox.ReadOnly = false;
        }

        private void UpdateBestWpm()
        {
            if (currentPlayer!= null 
                && currentRace != null 
                && currentPlayer.Records.Count > 0 
                && currentPlayer.Records.Count(x => x.Race.RaceId == currentRace.RaceId) > 0)
            {
                lblBestWpm.Text = string.Format("Best WPM: {0:f2}",
                                                currentPlayer.Records.Where(rec => rec.Race.RaceId == currentRace.RaceId)
                                                    .Max(x => x.CPM) / 5);
            }
            else
            {
                lblBestWpm.Text = "No Records.";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (start == null || !inProgress)
            {
                start = DateTime.Now;
                inProgress = true;

            }
            
            if (rtbRaceText.CurrentLine() > currentLine)
            {
                currentLine++;
                txtTypingBox.Location = new Point(txtTypingBox.Location.X, txtTypingBox.Location.Y + 20);
                rtbRaceText.Select(0, currentHighlightStart);
                rtbRaceText.SelectionColor = Color.LightBlue;
                rtbRaceText.Select(currentHighlightStart, currentHighlightLength);
            }

            if ((txtTypingBox.Text.Length > 0 && txtTypingBox.Text.Last() == ' ') 
                || (txtTypingBox.Text == currentWord && currentIndex == words.Count - 1))
            {         
                if (txtTypingBox.Text.TrimEnd() == currentWord)
                {
                    currentIndex++;

                    if (currentIndex == words.Count)
                    {
                        txtTypingBox.ReadOnly = true;
                        finish = DateTime.Now;
                        TimeSpan dur = finish.GetValueOrDefault().Subtract(start.GetValueOrDefault());
                        cpm = (currentText.Length/dur.TotalMilliseconds*1000*60);
                        wpm = (cpm / GameSettings.CharactersPerWord);
                        double accuracy = ((double)keyStrokeCount - mistakeCount) / keyStrokeCount * 100;
                        if (progressBar.ProgressBar != null)
                            progressBar.ProgressBar.Value = progressBar.Maximum;


                        if (currentPlayer.Records.Count(x=>x.Race.RaceId == currentRace.RaceId) == 0
                            || wpm > currentPlayer.Records.Where(x=>x.Race.RaceId == currentRace.RaceId).Max(x=>x.CPM))
                        {
                            bestRates[currentTextIndex] = wpm;
                            Record newRecord = new Record
                                                   {
                                                       Accuracy = accuracy,
                                                       CPM = cpm,
                                                       Player = currentPlayer,
                                                       Race = currentRace
                                                   };
                            records.Add(newRecord);
                            currentPlayer.Records.Add(newRecord);
                            currentRace.Records.Add(newRecord);

                            PlayerDao.SaveOrUpdate(currentPlayer);
                            RaceDao.SaveOrUpdate(currentRace);
                            RecordDao.SaveOrUpdate(newRecord);
                            

                            UpdateBestWpm();
                        }
                        lblCurrentWpm.Text = string.Format(WpmFormat, wpm);

                        MessageBox.Show(string.Format("Duration:\t{0} seconds\nCPM:\t{1:f2}\nWPM:\t{2:f2}\nAccuracy:\t{3:f2}%", 
                            dur.ToString(@"ss\.ff"),
                            cpm,
                            wpm,
                            accuracy));

                        
                        start = null;
                        currentIndex = 0;
                        inProgress = false;
                    }
                    else
                    {
                        
                        if (rtbRaceText.SelectionColor != Color.White) {
                            rtbRaceText.SelectionBackColor = Color.White;
                            rtbRaceText.SelectionColor = Color.Black;
                            rtbRaceText.SelectionFont = defaultFont;
                        }
                        currentHighlightStart += currentWord.Length + 1;
                        currentWord = words[currentIndex];
                        
                        currentHighlightLength = currentWord.Length;
                        

                        rtbRaceText.Select(currentHighlightStart, currentHighlightLength);
                        rtbRaceText.SelectionColor = Color.Blue;
                        rtbRaceText.SelectionFont = new Font(rtbRaceText.SelectionFont, FontStyle.Underline);
                        
                        txtTypingBox.Text = string.Empty;

                        

                        TimeSpan dur = DateTime.Now.Subtract(start.GetValueOrDefault());
                        cpm = (((double)currentHighlightStart / dur.TotalMilliseconds) * 1000 * 60);
                        wpm = (cpm / GameSettings.CharactersPerWord);

                        lblCurrentWpm.Text = string.Format(WpmFormat, wpm);
                        int progress = (int) (progressBar.Maximum*((double) currentHighlightStart/currentText.Length));
                        UpdateProgress(progress);

                    }
                }
            }

            if (txtTypingBox.Text.Length > 0 && txtTypingBox.Text.Length <= currentWord.Length && txtTypingBox.Text == currentWord.Substring(0, txtTypingBox.Text.Length))
            {
                txtTypingBox.BackColor = Color.White;
            }
            else if (txtTypingBox.Text.Length > 0)
            {
                txtTypingBox.BackColor = Color.LightCoral;
                if (previousLength < txtTypingBox.Text.Length)
                {
                    mistakeCount++;
                }
            }
            else
            {
                txtTypingBox.BackColor = Color.White;
            }

            previousLength = txtTypingBox.Text.Length;
        }

        /// <summary>
        /// Updates the progress bar.
        /// </summary>
        /// <param name="progress">The progress.</param>
        private void UpdateProgress(int progress)
        {
            if (progressBar.ProgressBar != null)
            {
                progressBar.ProgressBar.Value = progress;


                if ((double)progress / progressBar.Maximum < 0.75)
                {
                    progressBar.ForeColor = Color.Green;
                }
                else if ((double)progress / progressBar.Maximum < 0.9)
                {
                    progressBar.ProgressBar.ForeColor = Color.Yellow;
                }
                else
                {
                    progressBar.ForeColor = Color.Red;
                }
            }
        }

        private void txtTypingBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && inProgress)
            {
                txtTypingBox.Text = string.Empty;
            }
        }

        private void txtTypingBox_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue >= 'a' && e.KeyValue <= 'z') 
                || e.KeyValue >= 'A' && e.KeyValue <= 'Z'
                || e.KeyValue == ' ')
            {
                keyStrokeCount++;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPlayer = players.First(x => x.PlayerId == (comboBox1.SelectedValue as Player).PlayerId);
            UpdateBestWpm();
        }

    }
}
