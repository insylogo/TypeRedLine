using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeRacingDao.Entities;

namespace Shared
{
    /// <summary>
    /// Entity class for player records.
    /// </summary>
    [Serializable]
    public class RecordInfo
    {
        /// <summary>
        /// Gets or sets the record race.
        /// </summary>
        /// <value>
        /// The record race.
        /// </value>
        public RaceInfo Race { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        public PlayerInfo Player { get; set; }

        /// <summary>
        /// Gets or sets the WPM.
        /// </summary>
        /// <value>
        /// The WPM.
        /// </value>
        public double CPM { get; set; }

        /// <summary>
        /// Gets or sets the accuracy.
        /// </summary>
        /// <value>
        /// The accuracy.
        /// </value>
        public double Accuracy { get; set; }

        public RecordInfo()
        {
            
        }

        
    }

}
