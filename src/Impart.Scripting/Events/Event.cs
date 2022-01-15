using System;

namespace Impart.Scripting
{
    public static class Event
    {
        public static void Test<T>(this T classType)
        {
            if (typeof(T).IsAssignableFrom(typeof(IEvent)))
            {
                Console.WriteLine("aids");
            }
        }
    }
}