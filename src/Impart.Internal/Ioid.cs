namespace Impart.Internal
{
    /// <summary>Impart Object ID.</summary>
    public static class Ioid
    {
        private static int Count = 0;

        /// <summary>Generate and return a new IOID.</summary>
        public static int Generate()
        {
            Count++;
            return Count;
        }
    }
}