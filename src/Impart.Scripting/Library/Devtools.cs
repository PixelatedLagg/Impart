using System.Text;

namespace Impart.Scripting
{
    public static class DevTools
    {
        public static Invoke WriteLine(object obj)
        {
            return new Invoke($"console.log('{obj}');");
        }
        public static Invoke Warn(object obj)
        {
            return new Invoke($"console.warn('{obj}');");
        }
        public static Invoke TimeStart(object obj)
        {
            return new Invoke($"console.time('{obj}');");
        }
        public static Invoke TimeEnd(object obj)
        {
            return new Invoke($"console.timeEnd('{obj}');");
        }
        public static Invoke Group(object obj, params Invoke[] invokes)
        {
            StringBuilder builder = new StringBuilder($"console.group('{obj}');");
            foreach (Invoke invoke in invokes)
            {
                builder.Append(invoke.ToString());
            }
            builder.Append($"console.groupEnd('{obj}');");
            return new Invoke(builder.ToString());
        }
        public static Invoke Clear()
        {
            return new Invoke("console.clear();");
        }
    }
}