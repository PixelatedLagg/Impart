namespace Impart
{
    internal static class IOID
    {
        private static int Count = 0;
        internal static int Generate()
        {
            Count++;
            return Count;
        }
    }
}