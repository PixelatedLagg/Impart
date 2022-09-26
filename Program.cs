using Impart.Internal;
using Impart;
using System;

class Program
{
    public static void Main()
    {
        EListStorage<Text> storage = new EListStorage<Text>("<ul><li><p class=\"1\">aids</p></li><li><p class=\"2\">ahhh</p></li><li><p class=\"3\">eek</p></li></ul>", 1);
        EList<Text> list = storage.ToBuilder();
        foreach (Text t in list.Entries)
        {
            Console.WriteLine(t);
        }
    }
}