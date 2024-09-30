using CoreLib.Complex_Types;

namespace Events
{
    public static class TimeEvents
    {
        public static CoreEvent OnDayStart = new CoreEvent();
        public static CoreEvent OnDayEnd = new CoreEvent();
    }
}