using System.Text;

namespace Impart.Scripting
{
    /// <summary>Interact with developer tools such as the console.</summary>
    public static class DevTools
    {
        /// <summary>Writes the specified Object to the console and terminates the line.</summary>
        /// <param name="obj">The Object to write to the console.</param>
        public static Invoke WriteLine(object obj)
        {
            return new Invoke($"console.log('{obj}');");
        }

        /// <summary>Sends the console a warning.</summary>
        /// <param name="obj">The Object to use as the warning message.</param>
        public static Invoke Warn(object obj)
        {
            return new Invoke($"console.warn('{obj}');");
        }

        /// <summary>Begins a timer.</summary>
        /// <param name="obj">The Object to use as the timer name.</param>
        public static Invoke TimeStart(object obj)
        {
            return new Invoke($"console.time('{obj}');");
        }

        /// <summary>Ends a timer identified by the timer name.</summary>
        /// <param name="obj">The Object to use as the timer name.</param>
        public static Invoke TimeEnd(object obj)
        {
            return new Invoke($"console.timeEnd('{obj}');");
        }

        /// <summary>Writes a timer's current elapsed time, identified by the timer name.</summary>
        /// <param name="obj">The Object to use as the timer name.</param>
        public static Invoke WriteTime(object obj)
        {
            return new Invoke($"console.timeEnd('{obj}');");
        }

        /// <summary>Groups a series of Invokes together under a specified name.</summary>
        /// <param name="obj">The Object to use as the group name.</param>
        /// <param name="invokes">The Invokes to group together.</param>
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

        /// <summary>Clears the console.</summary>
        public static Invoke Clear()
        {
            return new Invoke("console.clear();");
        }

        /// <summary>Triggers a popup alert with a message.</summary>
        /// <param name="obj">The Object to use as the alert message.</param>
        public static Invoke Alert(object obj)
        {
            return new Invoke($"alert('{obj}');");
        }
    }
}