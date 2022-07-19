using System;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Store a list of Fonts.</summary>
    public class FontList : List<Font>
    {
        internal bool Changed = true;

        /// <summary>Creates a FontList instance.</summary>
        public FontList() : base() { }

        /// <summary>Add a Font to the FontList.</summary>
        /// <param name="font">The Font to add.</param>
        new public void Add(Font font)
        {
            Changed = true;
            base.Add(font);
        }

        /// <summary>Remove all Fonts from the FontList.</summary>
        new public void Clear()
        {
            Changed = true;
            base.Clear();
        }

        /// <summary>Add multiple Fonts to the FontList.</summary>
        /// <param name="fonts">The Fonts to add.</param>
        new public void AddRange(IEnumerable<Font> fonts)
        {
            Changed = true;
            base.AddRange(fonts);
        }

        /// <summary>Add a Font to the FontList at the specified index.</summary>
        /// <param name="index">The index to add the Font at.</param>
        /// <param name="font">The Font to add.</param>
        new public void Insert(int index, Font font)
        {
            Changed = true;
            base.Insert(index, font);
        }

        /// <summary>Add multiple Fonts to the FontList at the specified index.</summary>
        /// <param name="index">The index to add the Fonts at.</param>
        /// <param name="fonts">The Fonts to add.</param>
        new public void InsertRange(int index, IEnumerable<Font> fonts)
        {
            Changed = true;
            base.InsertRange(index, fonts);
        }

        /// <summary>Remove a Font from the FontList.</summary>
        /// <param name="font">The Font to remove.</param>
        new public void Remove(Font font)
        {
            Changed = true;
            base.Remove(font);
        }

        /// <summary>Remove all Fonts that meet specific conditions from the FontList.</summary>
        /// <param name="conditions">The Font conditions.</param>
        new public void RemoveAll(Predicate<Font> conditions)
        {
            Changed = true;
            base.RemoveAll(conditions);
        }

        /// <summary>Remove a Font from the FontList at the specified index.</summary>
        /// <param name="index">The index to remove the Font at.</param>
        new public void RemoveAt(int index)
        {
            Changed = true;
            base.RemoveAt(index);
        }

        /// <summary>Remove multiple Fonts from the FontList at the specified index.</summary>
        /// <param name="index">The index to remove the Fonts at.</param>
        /// <param name="count">The number of Fonts to remove after the index.</param>
        new public void RemoveRange(int index, int count)
        {
            Changed = true;
            base.RemoveRange(index, count);
        }
    }
}