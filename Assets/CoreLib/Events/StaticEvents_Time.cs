using CoreLib.Complex_Types;

namespace CoreLib.Events
{
    //Used by Updater for timers
    public static class StaticEvents_Time
    {
        public static readonly CoreEvent OnUpdate = new CoreEvent("Update");
        public static readonly CoreEvent OnFixedUpdate = new CoreEvent("FixedUpdate");
        public static readonly CoreEvent OnLateUpdate = new CoreEvent("LateUpdate");
    }
}