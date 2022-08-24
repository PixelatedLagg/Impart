using System.Text;
using System.Collections.Generic;

namespace Impart.Scripting
{
    /// <summary>Stores every Event for an IElement.</summary>
    public class EventManager
    {
        /// <summary>A List of all currently registered Events for the IElement.</summary>
        public List<Event> Events = new List<Event>();

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (Events.Count == 0)
            {
                return "";
            }
            StringBuilder result = new StringBuilder();
            foreach (Event e in Events)
            {
                result.Append(e);
            }
            return result.ToString();
        }
    }
}