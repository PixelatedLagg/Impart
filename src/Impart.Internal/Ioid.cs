namespace Impart.Internal
{
    public static class Ioid
    {
        private static int Count = 0;
        public static int Generate()
        {
            Count++;
            return Count;
        }
    }
}