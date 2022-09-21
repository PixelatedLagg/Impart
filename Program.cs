using Impart.Internal;
using Impart;
using System;

class Program
{
    public static void Main()
    {
        TextStorage text = new TextStorage("<p style=\"font-family: Verdana; margin: 10px;\" id=\"aids\" title=\"ahhhh\">ahhhh</p>");
        Text t = text.ToBuilder();
        foreach (Attr attr in t.Attrs)
        {
            Console.WriteLine(t);
        }
        Console.WriteLine(t.TextValue);
    }
}