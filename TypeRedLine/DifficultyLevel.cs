using System;

namespace TypeRedLine
{
    /// <summary>
    /// Enumerates the various difficulty levels for races.
    /// </summary>
    [SerializableAttribute]
    public enum DifficultyLevel
    {
        VeryEasy = 0,
        Easy,
        Medium,
        Hard,
        Insane,
        Impossible
    }
}
