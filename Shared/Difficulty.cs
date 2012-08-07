using System;

namespace Shared
{
    /// <summary>
    /// Enumerates the various difficulty levels for races.
    /// </summary>
    [SerializableAttribute]
    public enum Difficulty
    {
        VeryEasy = 0,
        Easy,
        Medium,
        Hard,
        Insane,
        Impossible
    }
}
