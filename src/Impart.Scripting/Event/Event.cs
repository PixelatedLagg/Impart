namespace Impart.Scripting
{
    public struct Event
    {
        public readonly EventType EventType;
        public readonly ExtAttrType ExtAttrChange = default;
        public readonly AttrType AttrChange = default;
        public readonly object[] Args;
        public Event(EventType eventType, ExtAttrType extAttrChange, params object[] args)
        {
            EventType = eventType;
            ExtAttrChange = extAttrChange;
            Args = args;
        }

        public Event(EventType eventType, AttrType attrChange, params object[] args)
        {
            EventType = eventType;
            AttrChange = attrChange;
            Args = args;
        }
    }
}