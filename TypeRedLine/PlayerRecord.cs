using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeRedLine
{
    /// <summary>
    /// Entity class for player records.
    /// </summary>
    [Serializable]
    public class PlayerRecord
    {
        /// <summary>
        /// Gets or sets the record race.
        /// </summary>
        /// <value>
        /// The record race.
        /// </value>
        public Race RecordRace { get; set; }


        /// <summary>
        /// Gets or sets the WPM.
        /// </summary>
        /// <value>
        /// The WPM.
        /// </value>
        public double CPM { get; set; }

        public PlayerRecord()
        {
            
        }
    }
}
