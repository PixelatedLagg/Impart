namespace Impart.Data
{
    public static class Letter
    {
        private static string AlphabetLower = "abcdefghijklmnopqrstuvwxyz";
        private static string AlphabetHigher = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string AlphabetInvariant = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private static string Numbers = "0123456789";
        private static string Specials = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";

        /// <summary>Randomly generate an alphabetical character.</summary>
        /// <param name="type">Whether to generate an uppercase or lowercase letter. (Default is set to generate both uppercase and lowercase.)</param>
        public static char Alphabet(bool? type = null)
        {
            if (type == null)
            {
                return AlphabetInvariant[(int)Impart.Data.Number.Range(0, 50)];
            }
            if (type.Value)
            {
                return AlphabetHigher[(int)Impart.Data.Number.Range(0, 25)];
            }
            else
            {
                return AlphabetLower[(int)Impart.Data.Number.Range(0, 25)];
            }
        }

        /// <summary>Randomly generate an numerical character.</summary>
        public static char Number()
        {
            return Numbers[(int)Impart.Data.Number.Range(0, 9)];
        }

        /// <summary>Randomly generate a special character.</summary>
        public static char Special()
        {
            return Specials[(int)Impart.Data.Number.Range(0, 27)];
        }
    }
}