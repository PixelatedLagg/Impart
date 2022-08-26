using Impart.Internal;

namespace Impart.Scripting
{
    /// <summary>Extensions to the Text class for the purpose of adding events.</summary>
    public static class TextExtensions
    {
        /// <summary>Set an event.</summary>
        /// <param name="text">The Text to add the event.</param>
        /// <param name="eventType">The EventType of the event.</param>
        /// <param name="function">The IFunction to call when the event is triggered.</param>
        public static void Set(this Text text, EventType eventType, IFunction function)
        {
            text._Events.Events.Add(new Event(eventType, function));
        }

        /// <summary>Get an event.</summary>
        /// <param name="text">The Text to get the event from.</param>
        /// <param name="attrType">The AttrType that is changed in the event.</param>
        /// <param name="args">The Attr value(s) to assign to the AttrType in the event.</param>
        public static Edit Get(this Text text, AttrType attrType, params object[] args)
        {
            return new Edit($"document.GetElementsByClassName('{text._IOID})[0].{ScriptingExtensions.GetEdit(attrType, args)}");
        }

        public static Edit GetMany(this Text text, AttrType attrType, params object[] args)
        {
            return new Edit($@"Array.from(document.GetElementsByClassName('{text._IOID}'){ScriptingExtensions.GetEdit(attrType, args)}");
        }
    }
}