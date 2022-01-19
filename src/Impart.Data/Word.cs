using System.Text;

namespace Impart.Data
{
    /// <summary>Randomly generate a word.</summary>
    public static class Word
    {
        /// <summary>Generate a word with the specified length.</summary>
        /// <param name="length">The length of the word to generate.</param>
        /// <param name="type">The type of letters to include in the word. (Default is every letter.)</param>
        public static string Length(int length, Letter.Types type = Letter.Types.Any)
        {
            StringBuilder result = new StringBuilder(length);
            switch (type)
            {
                case Letter.Types.Alphabet:
                    for (; length > 0; length--)
                    {
                        result.Append(Data.Letter.Alphabet());
                    }
                    break;
                case Letter.Types.Any:
                    for (; length > 0; length--)
                    {
                        result.Append(Data.Letter.Any());
                    }
                    break;
                case Letter.Types.Number:
                    for (; length > 0; length--)
                    {
                        result.Append(Data.Letter.Number());
                    }
                    break;
                case Letter.Types.Special:
                    for (; length > 0; length--)
                    {
                        result.Append(Data.Letter.Special());
                    }
                    break;
            }
            return result.ToString();
        }
    }
}