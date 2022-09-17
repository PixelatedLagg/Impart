using Impart.Internal;
using Impart;
using System;

class Program
{
    public static void Main()
    {
        Hsl hsl = (Hsl)StorageExtensions.GetColor("hsl(0, 100%, 0%)");
        Console.WriteLine((((float h, float s, float l))hsl));
    }
}