namespace Impart.Internal
{
	/// <summary>Extensions for Objects.</summary>
    public static class ObjectExtensions
    {
		/// <summary>Check if an Object is a number.</summary>
		/// <param name="o">The Object to check.</param>
        public static bool IsNumber(this object o)
		{
			return o is double || o is float || o is int || o is uint ||
			o is short || o is ushort || o is byte || o is long ||
			o is ulong;
		}
    }
}