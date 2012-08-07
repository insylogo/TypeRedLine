using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeRedLine
{
    class Race
    {
        public string Text { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public List<Tuple<string, int>> Records { get; set; }

        public Race(string text, DifficultyLevel difficulty)
        {
            Text = text;
            Difficulty = difficulty;
        }

        public bool IsRecord (int score)
        {
            return Records.Count(x => x.Item2 > score) < GameSettings.MaxRecords;
        }

        public void AddRecord (string player, int score)
        {
            if (IsRecord(score))
            {
                Records.Add(new Tuple<string, int>(player, score));
            }
        }
    }

}
