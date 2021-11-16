namespace Impart.Scripting
{
    public static class Extensions
    {
        public static Link SetEvent(this Link link, Event e)
        {
            switch (e)
            {
                case Event.OnClick:
                    break;
                case Event.OffClick:
                    break;
            }
            return link;
        }
    }
}