using System;
using System.Collections.Generic;
using System.Linq;

namespace TypeRedLine
{
    
    /// <summary>
    /// The basic Race entity
    /// </summary>
    [Serializable]
    public class Race
    {
        /// <summary>
        /// Gets or sets the text for the race.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the difficulty level for the race.
        /// </summary>
        /// <value>
        /// The difficulty.
        /// </value>
        public DifficultyLevel Difficulty { get; set; }


        /// <summary>
        /// Gets or sets the race's records.
        /// </summary>
        /// <value>
        /// The records.
        /// </value>
        public List<TrackRecord> Records { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Race"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="difficulty">The difficulty level.</param>
        public Race(string text, DifficultyLevel difficulty)
        {
            Text = text;
            Difficulty = difficulty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Race"/> class for serialization purposes.
        /// </summary>
        public Race()
        {
        }

        /// <summary>
        /// Determines whether the specified score is record.
        /// </summary>
        /// <param name="score">The score.</param>
        /// <returns>
        ///   <c>true</c> if the specified score is record; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRecord (double score)
        {
            return Records.Count(x => x.CPM > score) < GameSettings.MaxRecords;
        }

        /// <summary>
        /// Adds a new record, if the score is a record.
        /// </summary>
        /// <param name="player">The player name.</param>
        /// <param name="score">The score.</param>
        public void AddRecord (Player player, double score)
        {
            if (IsRecord(score))
            {
                Records.Add(new TrackRecord { CPM = score, Player = player });
            }
        }

    }

    
}
