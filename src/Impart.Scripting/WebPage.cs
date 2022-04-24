using Impart;

namespace Impart.Scripting
{
    public static class WebPageScripting
    {
        public static WebPage AddKeyEvent(this WebPage webPage, Key key)
        {
            webPage._Script.Append("//code here");
            return webPage;
        }
        public static WebPage AddClickEvent(this WebPage webPage, Click click)
        {
            webPage._Script.Append("//code here");
            return webPage;
        }
    }
}