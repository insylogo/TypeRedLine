using System;
using Shared;

namespace TypeRacingDao.Entities
{
    /// <summary>
    /// Entity class for player records.
    /// </summary>
    [Serializable]
    public class Record
    {
        /// <summary>
        /// Gets or sets the record id.
        /// </summary>
        /// <value>
        /// The record id.
        /// </value>
        public virtual int RecordId { get; set; }

        /// <summary>
        /// Gets or sets the record race.
        /// </summary>
        /// <value>
        /// The record race.
        /// </value>
        public virtual Race Race { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        public virtual Player Player { get; set; }

        /// <summary>
        /// Gets or sets the WPM.
        /// </summary>
        /// <value>
        /// The WPM.
        /// </value>
        public virtual double CPM { get; set; }

        /// <summary>
        /// Gets or sets the accuracy.
        /// </summary>
        /// <value>
        /// The accuracy.
        /// </value>
        public virtual double Accuracy { get; set; }

        public Record()
        {
            
        }
    }
}
