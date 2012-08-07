using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace TypeRacingDao.Entities
{
    
    /// <summary>
    /// The basic Race entity
    /// </summary>
    [Serializable]
    public class Race
    {
        public virtual string Name { get; set; }

        public virtual int RaceId { get; set; }

        /// <summary>
        /// Gets or sets the text for the race.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public virtual string Text { get; set; }

        /// <summary>
        /// Gets or sets the difficulty level for the race.
        /// </summary>
        /// <value>
        /// The difficulty.
        /// </value>
        public virtual Difficulty Difficulty { get; set; }


        /// <summary>
        /// Gets or sets the race's records.
        /// </summary>
        /// <value>
        /// The records.
        /// </value>
        public virtual IList<Record> Records { get; set; }


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
        public virtual bool IsRecord(double score)
        {
            return Records.Count(x => x.CPM > score) < GameSettings.MaxRecords;
        }

        /// <summary>
        /// Adds a new record, if the score is a record.
        /// </summary>
        /// <param name="player">The player name.</param>
        /// <param name="score">The score.</param>
        public virtual void AddRecord(Player player, double score)
        {
            if (IsRecord(score))
            {
                Records.Add(new Record { CPM = score, Player = player, Race = this });
            }
        }

    }

    
}
