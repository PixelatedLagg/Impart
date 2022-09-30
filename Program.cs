using Impart.Internal;
using Impart;
using System;

class Program
{
    public static void Main()
    {
        //TableStorage table = new TableStorage("<table><tr><td><p class=\"1\">a</p></td><td><p class=\"2\">b</p></td></tr></table>", 0);
        //Console.WriteLine(table.ToBuilder());
        //LinkStorage link = new LinkStorage("<a href=\"aids\"><img src=\"123\"></img></a>", 0);
        //Console.WriteLine(link.ToBuilder());
        int index = 1;
        Console.WriteLine(EListParser.Parse("<ul class=\"1\"><li><p class=\"2\">aids</p></li><li><p class=\"3\">test!</p></li></ul>", ref index));
    }
}