using System.Collections.Generic;
using System.Runtime.Serialization;
using Shared;

namespace TypeRacingService
{
    [DataContract]
    public class GameState
    {
        /// <summary>
        /// Gets or sets the state id.
        /// </summary>
        /// <value>
        /// The state id.
        /// </value>
        [DataMember]
        public int StateId { get; set; }

        /// <summary>
        /// Gets or sets the race.
        /// </summary>
        /// <value>
        /// The race.
        /// </value>
        [DataMember]
        public RaceInfo Race { get; set; }

        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        [DataMember]
        public IList<PlayerInfo> Players { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [DataMember]
        public RaceStatus Status { get; set; }
    }
}