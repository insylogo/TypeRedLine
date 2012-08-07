using System;
using System.Collections.Generic;

namespace TypeRedLine
{
    /// <summary>
    /// The default player entity.
    /// </summary>
    [Serializable]
    public class Player
    {
        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        /// <value>
        /// The records.
        /// </value>
        public List<PlayerRecord> Records { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        public Player(string name)
        {
            Name = name;
            Records = new List<PlayerRecord>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class for serialization purposes.
        /// </summary>
        public Player()
        {
            Records = new List<PlayerRecord>();
            Name = string.Empty;
        }

        /// <summary>
        /// Adds the record.
        /// </summary>
        /// <param name="race">The race the record was scored on.</param>
        /// <param name="score">The score.</param>
        public void AddRecord(Race race, double score)
        {
            if (Records == null)
            {
                Records = new List<PlayerRecord>();
            }
            Records.Add(new PlayerRecord{ RecordRace = race, CPM = score });
        }
    }
}
