using System;

namespace Csweb.Debug
{
    public static class Debug
    {
        public static event Action<Log> CSWebObjEvent;
        public static event Action<Log> IDStyleEvent;
        internal static void CallEvent(Log log)
        {
            CSWebObjEvent?.Invoke(log);
        }
    }
}