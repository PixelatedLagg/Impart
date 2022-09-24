using Impart.Internal;
using Impart;
using System;

class Program
{
    public static void Main()
    {
        EList<Text> list = new EList<Text>(EListType.Unordered, "aids", "ahhh", "eek");
        Console.WriteLine(list);
    }
}