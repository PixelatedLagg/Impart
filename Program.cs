using System.Text;
using Impart;
using System;
using Impart.Internal;
using System.Threading.Tasks;

class Program
{
    public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();
    public static async Task MainAsync()
    {
        new test();
        //Console.WriteLine((await new test().ToStringAsync()));
    }
}
class test : WebPage
{
    public test() : base()
    {
        Text t = new Text("aids");
        t.ExtAttrs.Add(new ExtAttr(ExtAttrType.ID, "hepatitis!"));
        Text t2 = new Text("test2");
        t2.ExtAttrs.Add(new ExtAttr(ExtAttrType.ID, "test"));
        Add(t);
        Add(t2);
        Console.WriteLine(Get<Text>(x => x.ID == "hepatitis!").TextValue);
    }
}