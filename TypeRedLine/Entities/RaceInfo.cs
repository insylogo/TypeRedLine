using System;
using System.Collections.Generic;
using System.Linq;
using TypeRacingDao.Entities;

namespace Shared
{
    
    /// <summary>
    /// The basic Race entity
    /// </summary>
    [Serializable]
    public class RaceInfo
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
        public Difficulty Difficulty { get; set; }


        /// <summary>
        /// Gets or sets the race's records.
        /// </summary>
        /// <value>
        /// The records.
        /// </value>
        public List<RecordInfo> Records { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="RaceInfo"/> class for serialization purposes.
        /// </summary>
        public RaceInfo()
        {
        }

        public RaceInfo(Race race)
        {
            Text = race.Text;
            Records = new List<RecordInfo>();

            foreach (var record in race.Records)
            {
                Records.Add(new RecordInfo { Accuracy = record.Accuracy,
                    CPM = record.CPM, 
                    Race = this, 
                    Player = new PlayerInfo { Name = record.Player.Name, 
                        PlayerId = record.Player.PlayerId, 
                        Records = this.Records } });
            }
        }

        /// <summary>
        /// Determines whether the specified score is record.
        /// </summary>
        /// <param name="score">The score.</param>
        /// <returns>
        ///   <c>true</c> if the specified score is record; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRecord(double score)
        {
            return Records.Count(x => x.CPM > score) < GameSettings.MaxRecords;
        }

        /// <summary>
        /// Adds a new record, if the score is a record.
        /// </summary>
        /// <param name="player">The player name.</param>
        /// <param name="score">The score.</param>
        public void AddRecord(PlayerInfo player, double score)
        {
            if (IsRecord(score))
            {
                Records.Add(new RecordInfo { CPM = score, Player = player, Race = this });
            }
        }

    }

    
}
