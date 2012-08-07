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
        public Dictionary<Player, int> Positions { get; set; }

        /// <summary>
        /// Gets or sets the accuracies.
        /// </summary>
        /// <value>
        /// The accuracy.
        /// </value>
        public Dictionary<Player, double> Accuracy { get; set; }

        /// <summary>
        /// Gets or sets the streaks.
        /// </summary>
        /// <value>
        /// The streak.
        /// </value>
        public Dictionary<Player, int> Streak { get; set; }
    }
}
