using System;

namespace Csweb
{
    public static class Debug
    {
        public static event Action<Log> ObjectEvent;
        internal static void CallObjectEvent(Log log)
        {
            ObjectEvent?.Invoke(log);
        }
    }
}