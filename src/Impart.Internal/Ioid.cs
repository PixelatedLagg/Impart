namespace Impart.Internal
{
    /// <summary>Impart Object ID.</summary>
    public static class Ioid
    {
        private static int Count = 0;
        private static double OtherCount = 0;

        /// <summary>Generate and return a new IOID.</summary>
        public static int Generate()
        {
            Count++;
            OtherCount = Count;
            return Count;
        }

        /// <summary>Generate and return a new IOID for multiple uses with a single instance.</summary>
        public static double GenerateOtherUnique()
        {
            OtherCount *= 0.1;
            return OtherCount;
        }
    }
}