namespace Impart.Scripting
{
    public struct Event
    {
        private string Render;
        public Event(EventType eventType, IFunction function)
        {
            switch (eventType)
            {
                case EventType.Click:
                    Render = $"onclick=\"{function}\"";
                    break;
                case EventType.Hover:
                    Render = $"onmouseover=\"{function}\"";
                    break;
                default:
                    Render = "";
                    break;
            }
        }
        public override string ToString()
        {
            return Render;
        }
    }
}