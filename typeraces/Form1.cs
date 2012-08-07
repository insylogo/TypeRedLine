using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shared;
using TypeRedLine.Properties;

namespace TypeRedLine
{
    public partial class Form1 : Form
    {
        private string currentText;
        private int currentTextIndex;
        private List<string> words;
        private string currentWord;
        private int currentIndex;
        private Font defaultFont;

        private int currentHighlightStart;
        private int currentHighlightLength;
        private int currentLine;
        private List<string> texts;

        private Random gen;
        private DateTime? start;
        private DateTime? finish;

        private List<double> bestRates;


        public Form1()
        {
            InitializeComponent();
            texts = new List<string>
                        {
                            "Zerp to the burp, have a durp; love a twerp. Amerikkka h8s u. But I don't!",
                            "The witchdoctor is the weirdest class in all of diablo 3. Fuck diablo 3 though, it kind of sucks.",
                            "Don't even worry about it, bro. It's all good in the end.",
                            "Roronoa Zoro is a pirate, former bounty hunter, and one of the main protagonists of One Piece. He is the first member to join the Straw Hat Pirates and to date is considered the largest threat and most dangerous member in the crew after Luffy. His fame as a master swordsman and his great strength, along with the actions of his captain, sometimes leads others to believe that he must be the true captain and is widely thought to be the first mate by those outside the crew. He is one of the top three fighters in the crew, and his dream is to become the greatest swordsman in the world.",
                            "Monkey D. Luffy is a pirate and the main protagonist of the anime and manga, One Piece. He is the son of the Revolutionary Army's commander, Monkey D. Dragon, the grandson of the famed Marine, Monkey D. Garp, the foster son of a mountain bandit, Curly Dadan, and the adopted brother of the late \"Fire Fist\" Portgas D. Ace and Sabo. His life long goal is to become the Pirate King by finding the legendary treasure left behind by the Pirate King, Gol D. Roger. He believes that being Pirate King means one has the most freedom in the world. He has eaten the Gomu Gomu no Mi. As the founder and captain of the Straw Hat Pirates, he is the first member that makes up the crew, as well as one of its top three fighters."
                        };
            gen = new Random();
            defaultFont = richTextBox1.Font;

            bestRates = new List<double>();

            foreach (var t in texts)
            {
                bestRates.Add(0.0);
            }


            toolStripProgressBar1.Style = ProgressBarStyle.Continuous;
            toolStripProgressBar1.ForeColor = Color.Green;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Height = 268;

            textBox1.Location = new Point(0, 20);
            textBox1.Focus();

            label1.Visible = true;
            label2.Visible = true;

            currentTextIndex = gen.Next(texts.Count);
            label1.Text = Resources.WPMString;
            label2.Text = string.Format("Best Score: {0:f2}", bestRates[currentTextIndex]);

            currentText = texts[currentTextIndex];
            char[] space = {' '};
            words = new List<string>(currentText.Split(space));

            start = null;
            richTextBox1.Text = currentText;
            
            
            currentLine = 0;
            currentIndex = 0;
            currentWord = words[currentIndex] ?? string.Empty;

            currentHighlightStart = 0;
            currentHighlightLength = currentWord.Length;

            textBox1.Text = string.Empty;

            toolStripProgressBar1.Value = 0;

            richTextBox1.Select(0, richTextBox1.Text.Length);
            richTextBox1.SelectionBackColor = Color.White;
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.SelectionFont = defaultFont;

            richTextBox1.Select(currentHighlightStart, currentHighlightLength);
            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Underline);
            
            textBox1.ReadOnly = false;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (start == null)
            {
                start = DateTime.Now;

            }
            
            if (richTextBox1.CurrentLine() > currentLine)
            {
                currentLine++;
                textBox1.Location = new Point(textBox1.Location.X, textBox1.Location.Y + 20);
                richTextBox1.Select(0, currentHighlightStart);
                richTextBox1.SelectionColor = Color.White;
                richTextBox1.Select(currentHighlightStart, currentHighlightLength);
            }

            if ((textBox1.Text.Length > 0 && textBox1.Text.Last() == ' ') 
                || (textBox1.Text == currentWord && currentIndex == words.Count - 1))
            {         
                if (textBox1.Text.TrimEnd() == currentWord)
                {
                    currentIndex++;

                    if (currentIndex == words.Count)
                    {
                        textBox1.ReadOnly = true;
                        finish = DateTime.Now;
                        TimeSpan dur = finish.GetValueOrDefault().Subtract(start.GetValueOrDefault());
                        double cpm = ((double)currentText.Length/dur.TotalMilliseconds*1000*60);
                        double wpm = (cpm / GameSettings.CharactersPerWord);

                        if (toolStripProgressBar1.ProgressBar != null)
                            toolStripProgressBar1.ProgressBar.Value = toolStripProgressBar1.Maximum;


                        if (wpm > bestRates[currentTextIndex])
                        {
                            bestRates[currentTextIndex] = wpm;
                            label2.Text = string.Format("Best Score: {0:f2}", wpm);
                        }
                        label1.Text = string.Format("WPM: {0:f2}", wpm);

                        MessageBox.Show(string.Format("Duration:\t{0} seconds\nCPM:\t{1:f2}\nWPM:\t{2:f2}", 
                            dur.ToString(@"ss\.ff"),
                            cpm,
                            wpm));

                        
                        start = null;
                        currentIndex = 0;
                    }
                    else
                    {
                        
                        

                        
                        if (richTextBox1.SelectionColor != Color.White) {
                            richTextBox1.SelectionBackColor = Color.White;
                            richTextBox1.SelectionColor = Color.Black;
                            richTextBox1.SelectionFont = defaultFont;
                        }
                        currentHighlightStart += currentWord.Length + 1;
                        currentWord = words[currentIndex];
                        
                        currentHighlightLength = currentWord.Length;
                        

                        richTextBox1.Select(currentHighlightStart, currentHighlightLength);
                        richTextBox1.SelectionColor = Color.Blue;
                        richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Underline);
                        
                        textBox1.Text = string.Empty;

                        

                        TimeSpan dur = DateTime.Now.Subtract(start.GetValueOrDefault());
                        double cpm = (((double)currentHighlightStart / dur.TotalMilliseconds) * 1000 * 60);
                        double wpm = (cpm / GameSettings.CharactersPerWord);

                        label1.Text = string.Format("WPM: {0:f2}", wpm);

                        
                        int progress = (int) (toolStripProgressBar1.Maximum*((double) currentHighlightStart/currentText.Length));


                        if (toolStripProgressBar1.ProgressBar != null)
                        {
                            toolStripProgressBar1.ProgressBar.Value = progress;


                            if ((double) progress / toolStripProgressBar1.Maximum < 0.75)
                            {
                                toolStripProgressBar1.ForeColor = Color.Green;
                            }
                            else if ((double) progress / toolStripProgressBar1.Maximum < 0.9)
                            {
                                toolStripProgressBar1.ProgressBar.ForeColor = Color.Yellow;
                            }
                            else
                            {
                                toolStripProgressBar1.ForeColor = Color.Red;
                            }
                        }

                        //= string.Format("CPM: {0}\t WPM: {1}", 
                         //   cpm,
                         //   wpm);

                    }
                }
            }

            if (textBox1.Text.Length > 0 && textBox1.Text.Length <= currentWord.Length && textBox1.Text == currentWord.Substring(0, textBox1.Text.Length))
            {
                textBox1.BackColor = Color.White;
            }
            else if (textBox1.Text.Length > 0)
            {
                textBox1.BackColor = Color.LightCoral;

            }
            else
            {
                textBox1.BackColor = Color.White;
            }

  
        }

    }
}
