namespace Impart
{
    internal class Utilities
    {
        internal static int FindLength(Text[] args)
        {
            int result = 0;
            foreach (Text t in args)
            {
                if (t.id == null)
                {
                    result += $"%^    <p{t.attributeBuilder.ToString()}{t.style}>{t.text}</p>".Length;
                }
                else
                {
                    result += $"%^    <p id=\"{t.id}\"{t.attributeBuilder.ToString()}{t.style}>{t.text}</p>".Length;
                }
            }
            return result;
        }
    }
}