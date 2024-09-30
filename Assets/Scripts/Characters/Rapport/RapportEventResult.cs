using CoreLib.Complex_Types;

namespace Characters
{
    /// <summary>
    /// Describes the result of a Rapport Event.
    /// </summary>
    public class RapportEventResult
    {
        public int DayOfEvent = 0;
        public DefaultDict<string, bool> Flags = new DefaultDict<string, bool>(() => false);
    }
}