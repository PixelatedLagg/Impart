using Impart.Internal;
using Impart;
using System;

class Program
{
    public static void Main()
    {
        HeaderStorage text = new HeaderStorage("<h1 style=\"font-family: Verdana; margin: 10px;\" id=\"aids\" title=\"ahhhh\">ahhhh</h1>");
        Header t = text.ToBuilder();
        foreach (Attr attr in t.Attrs)
        {
            Console.WriteLine(t);
        }
        Console.WriteLine(t.TextValue);
    }
}