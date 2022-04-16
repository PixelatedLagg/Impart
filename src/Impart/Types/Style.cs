using System;
using System.Text;

namespace Impart
{
    /// <summary>Style an ID, element, or class.</summary>
    public sealed class Style : IDisposable
    {
        private StringBuilder textBuilder;
        private string id;

        /// <value>The ID value of the Style.</value>
        public string ID
        {
            get
            {
                return id;
            }
        }

        /// <summary>Creates a Style instance with <paramref name="path"/> as the style type and <paramref name="id"/> as the ID/element/class..</summary>
        /// <returns>A Style instance.</returns>
        /// <param name="style">The style type.</param>
        /// <param name="id">The ID/element/class.</param>
        public Style(StyleType style, string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ImpartError("ID/Class/Element cannot be null!");
            }
            textBuilder = new StringBuilder(1000);
            switch (style)
            {
                case StyleType.IDStyle:
                    textBuilder.Append($"#{id} {{");
                    break;
                case StyleType.EStyle:
                    textBuilder.Append($"{id} {{");
                    break;
                case StyleType.ClassStyle:
                    textBuilder.Append($".{id} {{");
                    break;
            }
            this.id = id;
        }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            return $"{textBuilder.ToString()}}}";
        }

        /// <summary>Dispose of all the associated variables in the Style instance. Included to support using() statements.</summary>
        public void Dispose()
        {
            id = "";
            textBuilder.Clear();
        }
    }

    /// <summary>List of every type of Style. Includes ID, element, and class.</summary>
    public enum StyleType
    {
        IDStyle = 0,
        EStyle = 1,
        ClassStyle = 2
    }
}