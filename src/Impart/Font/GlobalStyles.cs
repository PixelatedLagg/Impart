using System.Collections.Generic;

namespace Impart
{
    /// <summary>Manage both global external Styles and global Styles.</summary>
    public static class GlobalStyles
    {
        /// <summary>The Styles that apply to every Webpage.</summary>
        public static List<Style> Styles = new List<Style>();

        /// <summary>The external Styles that apply to every Webpage.</summary>
        public static List<string> ExternalStyles = new List<string>();

        /// <summary>Add a Style to every WebPage.</summary>
        /// <param name="style">The Style to add.</param>
        public static void Add(Style style)
        {
            Styles.Add(style);
        }

        /// <summary>Add an external Style to every WebPage.</summary>
        /// <param name="url">The URL of the external Style to add.</param>
        public static void AddExternal(string url)
        {
            ExternalStyles.Add(url);
        }

        /// <summary>Remove a Style from being added to every WebPage.</summary>
        /// <param name="index">The index of the Style to remove.</param>
        public static void Remove(int index)
        {
            Styles.RemoveAt(index);
        }

        /// <summary>Remove an external Style from being added to every WebPage.</summary>
        /// <param name="index">The index of the URL of the external Style to be removed.</param>
        public static void RemoveExternal(int index)
        {
            ExternalStyles.RemoveAt(index);
        }
    }
}