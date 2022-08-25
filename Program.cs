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
        
    }
}