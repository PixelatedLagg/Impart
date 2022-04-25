namespace Impart.Internal
{
    public static class Number
    {
        public static bool IsNumber(object o)
		{
			return o is double || o is float || o is int || o is uint ||
			o is short || o is ushort || o is byte || o is long ||
			o is ulong;
		}
    }
}