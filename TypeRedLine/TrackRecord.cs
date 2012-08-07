using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeRedLine
{
    /// <summary>
    /// Entity class of track records.
    /// </summary>
    public class TrackRecord
    {
        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        public Player Player { get; set; }

        /// <summary>
        /// Gets or sets the characters per minute.
        /// </summary>
        /// <value>
        /// The CPM.
        /// </value>
        public double CPM { get; set; }

        public double WPM
        {
            get { return CPM/GameSettings.CharactersPerWord; }
        }
    }
}
