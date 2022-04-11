using Impart;

namespace Demo
{
    public class Program
    {
        public static void Main()
        {
            Website website = new Website(new ExamplePage());
            website.Start();
        }
    }
    public class ExamplePage : WebPage
    {
        public ExamplePage() : base()
        {
            AddText(new Text("Hello World!"));
        }
    }
}