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
        Console.WriteLine((await new test().ToStringAsync()));
    }
}
class test : WebPage
{
    public test() : base()
    {
        AddText("eek!");
        GetIElement<Image>(x => x.)
    }
}