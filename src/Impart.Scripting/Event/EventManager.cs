using System.Collections.Generic;

namespace Impart.Scripting
{
    public class EventManager
    {
        public List<Event> Events = new List<Event>();
        public bool InUse()
        {
            if (Events.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}