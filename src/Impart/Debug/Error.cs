using System;

namespace Impart
{
    public class ImpartError : Exception
    {
        public ImpartError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{{ Impart Error - {error} }}");
            Environment.Exit(0);
        }
    }
}