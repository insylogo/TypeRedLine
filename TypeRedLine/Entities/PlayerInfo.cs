using System;
using System.Collections.Generic;

namespace Shared
{
    /// <summary>
    /// The default player entity.
    /// </summary>
    [Serializable]
    public class PlayerInfo
    {

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        /// <value>
        /// The player id.
        /// </value>
        public int PlayerId { get; set; }

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
        public List<RecordInfo> Records { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInfo"/> class for serialization purposes.
        /// </summary>
        public PlayerInfo()
        {

        }

        /// <summary>
        /// Adds the record.
        /// </summary>
        /// <param name="race">The race the record was scored on.</param>
        /// <param name="score">The score.</param>
        public virtual void AddRecord(RaceInfo race, double score)
        {
            if (Records == null)
            {
                Records = new List<RecordInfo>();
            }
            Records.Add(new RecordInfo { Race = race, CPM = score, Player = this });
        }
    }
}
