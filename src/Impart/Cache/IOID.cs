namespace Impart
{
    internal static class Ioid
    {
        private static int Count = 0;
        internal static int Generate()
        {
            Count++;
            return Count;
        }
    }
}