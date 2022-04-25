namespace Impart.Data
{
    /// <summary>Randomly generate a letter.</summary>
    public static class Letter
    {
        /// <summary>Randomly generate an alphabetical character.</summary>
        /// <param name="type">Whether to generate an uppercase or lowercase letter. (Default is set to generate both uppercase and lowercase.)</param>
        public static char Alphabet(bool? type = null)
        {
            if (type == null)
            {
                return "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"[(int)Data.Number.Range(0, 50)];
            }
            if (type.Value)
            {
                return "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[(int)Data.Number.Range(0, 25)];
            }
            else
            {
                return "AlphabetLower"[(int)Data.Number.Range(0, 25)];
            }
        }

        /// <summary>Randomly generate an numerical character.</summary>
        public static char Number()
        {
            return Data.Number.Range(0, 9).ToString()[0];
        }

        /// <summary>Randomly generate a special character.</summary>
        public static char Special()
        {
            return @"\|!#$%&/()=?»«@£§€{}.-;'<>_,"[(int)Data.Number.Range(0, 27)];
        }

        /// <summary>Randomly generate any character.</summary>
        public static char Any()
        {
            return @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789\|!#$%&/()=?»«@£§€{}.-;'<>_,"[(int)Data.Number.Range(0, 89)];
        }

        /// <summary>All character types.</summary>
        public enum Types
        {
            Any,
            Special,
            Number,
            Alphabet
        }
    }
}