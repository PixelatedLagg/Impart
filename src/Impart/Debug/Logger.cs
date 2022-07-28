using System;
using System.Diagnostics;

namespace Impart
{
    /// <summary>Log messages/warnings to the console.</summary>
    public static class Logger
    {
        /// <summary>Log a message to the console. This method will rarely be called by Impart automatically unless you define the compile-time constant: DEBUG.</summary>
        /// <param name="log">The message to log.</param>
        public static void Info(string log)
        {
            Console.WriteLine($"[Impart - Info]({DateTime.Now.ToString("hh:mm:ss")}) : {log}");
        }

        /// <summary>Log a warning to the console. Impart will automatically call this method when something of mild concern happens.</summary>
        /// <param name="warning">The warning to log.</param>
        public static void Warning(string warning)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write("[Impart - Warning]");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"({DateTime.Now.ToString("hh:mm:ss")}) : {warning}");
        }

        /// <summary>Log a critical warning to the console. Impart will automatically call this method when something severely bad happens.</summary>
        /// <param name="warning">The critical warning to log.</param>
        public static void CriticalWarning(string warning)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write("[Impart - Critical Warning]");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write($"({DateTime.Now.ToString("hh:mm:ss")}) : {warning} {new StackTrace(new StackFrame(1))}");
        }
    }
}