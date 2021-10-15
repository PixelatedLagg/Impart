using System;

namespace Csweb
{
    public static class Debug
    {
        public static event Action<Log> CSWebObjEvent;
        public static event Action<Log> IDStyleEvent;
        internal static void CallCSWebEvent(Log log)
        {
            CSWebObjEvent?.Invoke(log);
        }
        internal static void CallIDStyleEvent(Log log)
        {
            IDStyleEvent?.Invoke(log);
        }
    }
}