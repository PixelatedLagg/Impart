using Impart.Internal;
using Impart;
using System;

class Program
{
    public static void Main()
    {
        //TableStorage table = new TableStorage("<table><tr><td><p class=\"1\">a</p></td><td><p class=\"2\">b</p></td></tr></table>", 0);
        //Console.WriteLine(table.ToBuilder());
        LinkStorage link = new LinkStorage("<a href=\"aids\"><img class=\"123\">testing!!!</img></a>", 0);

        Console.WriteLine(link.ToBuilder());
    }
}