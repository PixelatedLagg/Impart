using System.Text;
using System.Collections.Generic;

namespace Impart.Scripting
{
    public class EventManager
    {
        public List<Event> Events = new List<Event>();
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