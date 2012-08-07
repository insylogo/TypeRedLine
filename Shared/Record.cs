using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared
{
    /// <summary>
    /// Entity class for player records.
    /// </summary>
    [Serializable]
    public class Record
    {
        /// <summary>
        /// Gets or sets the record race.
        /// </summary>
        /// <value>
        /// The record race.
        /// </value>
        public virtual Race Race { get; set; }

        public virtual Player Player { get; set; }

        /// <summary>
        /// Gets or sets the WPM.
        /// </summary>
        /// <value>
        /// The WPM.
        /// </value>
        public virtual double CPM { get; set; }

        public virtual double Accuracy { get; set;
        }
        public Record()
        {
            
        }
    }
}
