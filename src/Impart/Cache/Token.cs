namespace Impart
{
    public struct Token
    {
        public (long count, WebPage page) Current;
        public (long count, WebPage page) Previous;
        public Token(WebPage WebPage)
        {
            Current.count = 0;
            Current.page = null;
            Previous.count = 0;
            Previous.page = null;
        }
        public void Change(WebPage WebPage)
        {
            Current.count++;
            Previous.count++;
            Previous.page = Current.page;
            Current.page = WebPage;
        }
        public void Check(WebPage WebPage)
        {
            if (WebPage == Current.page)
            {
                return;
            }
            Change(WebPage);
        }
    }
}