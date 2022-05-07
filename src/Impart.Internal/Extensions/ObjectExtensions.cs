namespace Impart.Internal
{
    public static class ObjectExtensions
    {
        public static bool IsNumber(this object o)
		{
			return o is double || o is float || o is int || o is uint ||
			o is short || o is ushort || o is byte || o is long ||
			o is ulong;
		}
    }
}