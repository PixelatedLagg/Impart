using Impart.Internal;
using Impart;
using System;

class Program
{
    public static void Main()
    {
        TextStorage text = new TextStorage("<p style=\"font-family: Verdana;\" id=\"aids\" title=\"ahhhh\">ahhhh</p>");
        Text t = text.ToBuilder();
        Console.WriteLine(t.TextValue);
    }
}