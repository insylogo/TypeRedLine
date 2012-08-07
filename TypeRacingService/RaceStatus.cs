using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared
{
    public class RaceStatus
    {
        /// <summary>
        /// Gets or sets the positions.
        /// </summary>
        /// <value>
        /// The positions.
        /// </value>
        public Dictionary<PlayerInfo, int> Positions { get; set; }

        /// <summary>
        /// Gets or sets the accuracies.
        /// </summary>
        /// <value>
        /// The accuracy.
        /// </value>
        public Dictionary<PlayerInfo, double> Accuracy { get; set; }

        /// <summary>
        /// Gets or sets the streaks.
        /// </summary>
        /// <value>
        /// The streak.
        /// </value>
        public Dictionary<PlayerInfo, int> Streak { get; set; }
    }
}
