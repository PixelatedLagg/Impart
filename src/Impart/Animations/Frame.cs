namespace Impart
{
    public struct Frame
    {
        public readonly Percent Position;
        public readonly ChangeType ChangeType;
        public readonly object Change;
        public Frame(Percent position, ChangeType changeType, object change)
        {
            Position = position;
            ChangeType = changeType;
            Change = change;
        }
    }
}