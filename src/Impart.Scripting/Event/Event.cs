using Impart.Internal;

namespace Impart.Scripting
{
    /// <summary>Stores an Event.</summary>
    public struct Event
    {
        private string Render;

        /// <summary>Creates an Event instance.</summary>
        /// <param name="eventType">The EventType.</param>
        /// <param name="function">The IFunction to call when the Event is triggered.</param>
        public Event(EventType eventType, IFunction function)
        {
            Render = $" on{ScriptingExtensions.GetEventName(eventType)}=\"{function}\"";
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return Render;
        }
    }
}