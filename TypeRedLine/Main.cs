using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Shared;
using TypeRedLine.Properties;

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

            Type[] types = { typeof(Player), typeof(Race) };
            XmlSerializer serializer = new XmlSerializer(typeof(List<Race>), types);
            var reader = new StreamReader(new FileStream(Application.StartupPath + "\\Races\\races.trl", FileMode.Open, FileAccess.Read));

            races = serializer.Deserialize(reader) as List<Race>;

            reader.Close();

            gen = new Random();

            defaultFont = rtbRaceText.Font;

            bestRates = new List<double>();

            foreach (var t in races)
            {
                bestRates.Add(0.0);
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
            lblBestWpm.Text = string.Format(BestWpmFormat, bestRates[currentTextIndex]);

            currentText = races[currentTextIndex].Text;
            char[] space = { ' ' };
            words = new List<string>(currentText.Split(space));


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

                        if (progressBar.ProgressBar != null)
                            progressBar.ProgressBar.Value = progressBar.Maximum;


                        if (wpm > bestRates[currentTextIndex])
                        {
                            bestRates[currentTextIndex] = wpm;
                            lblBestWpm.Text = string.Format(BestWpmFormat, wpm);
                        }
                        lblCurrentWpm.Text = string.Format(WpmFormat, wpm);

                        MessageBox.Show(string.Format("Duration:\t{0} seconds\nCPM:\t{1:f2}\nWPM:\t{2:f2}\nAccuracy:\t{3:f2}%", 
                            dur.ToString(@"ss\.ff"),
                            cpm,
                            wpm,
                            ((double)keyStrokeCount - mistakeCount) / keyStrokeCount * 100));

                        
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

    }
}
