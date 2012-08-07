using System;
using System.Collections.Generic;

namespace TypeRacingDao.Entities
{
    /// <summary>
    /// The default player entity.
    /// </summary>
    [Serializable]
    public class Player
    {

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        /// <value>
        /// The player id.
        /// </value>
        public virtual int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        /// <value>
        /// The records.
        /// </value>
        public virtual IList<Record> Records { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class for serialization purposes.
        /// </summary>
        public Player()
        {

        }

        /// <summary>
        /// Adds the record.
        /// </summary>
        /// <param name="race">The race the record was scored on.</param>
        /// <param name="score">The score.</param>
        public virtual void AddRecord(Race race, double score)
        {
            if (Records == null)
            {
                Records = new List<Record>();
            }
            Records.Add(new Record { Race = race, CPM = score, Player = this });
        }
    }
}
