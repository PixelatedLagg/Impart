using System.Text;
using Impart;
using System;
using Impart.Scripting;
using Impart.Internal;
using System.Threading.Tasks;

class Program
{
    public static void Main(string[] args)
    {
        Website w = new Website(new test());
        w.Start();
    }
}
class test : WebPage
{
    public test() : base()
    {
        Text text = new Text("test!");
        text.Attrs.Add(new Attr(AttrType.FontSize, 60));
        text.Set(EventType.Click, text.Get(AttrType.BackgroundColor, Rgb.Blue));
        Add(text);
    }
}