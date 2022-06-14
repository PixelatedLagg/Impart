namespace Impart.Data
{
    public static class Emoji
    {
        public static string Any()
        {
            return $"&#{Number.Range(100000, 999999)};";
        }
    }
}