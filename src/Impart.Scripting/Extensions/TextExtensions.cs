using Impart.Internal;

namespace Impart.Scripting
{
    public static class TextExtensions
    {
        public static void Set(this Text text, EventType eventType, IFunction function)
        {
            text._Events.Events.Add(new Event(eventType, function));
        }
        public static Edit Get(this Text text, AttrType attrType, params object[] args)
        {
            return ScriptingExtensions.Get(text, attrType, args);
        }
    }
}