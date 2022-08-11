namespace Impart.Scripting
{
    public struct Event
    {
        public readonly Trigger Trigger;
        public readonly Change Change;
        public readonly object[] Args;
        public Event(Trigger trigger, Change change, params object[] args)
        {
            Trigger = trigger;
            Change = change;
            Args = args;
        }
    }
}