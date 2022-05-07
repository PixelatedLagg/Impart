using System.IO;

namespace Impart.Internal
{
    public static class StringExtensions
    {
        public static string SkipWhiteSpace(this string source, ref int index, int length, out char ch)
		{
			ch = default;
			while (index < length)
			{
				ch = source[index];
				if (!char.IsWhiteSpace(ch)) break;
				index++;
			}
			if (index >= length)
			{
				ch = default;
				return "Unexpected end of input.";
			}
			return null;
		}
		public static string SkipWhiteSpace(this TextReader stream, out char ch)
		{
			ch = default;
			int c = stream.Peek();
			while (c != -1)
			{
				ch = (char)c;
				if (!char.IsWhiteSpace(ch)) break;
				stream.Read();
				c = stream.Peek();
			}
			if (c == -1)
			{
				ch = default;
				return "Unexpected end of input.";
			}
			return null;
		}
    }
}