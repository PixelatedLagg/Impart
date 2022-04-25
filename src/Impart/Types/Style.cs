using System;

namespace Impart
{
    /// <summary>Style Elements with a certain ID.</summary>
    public sealed class Style : IDisposable
    {
        private string _ID;

        /// <value>The ID value of the Style.</value>
        public string ID
        {
            get
            {
                return _ID;
            }
        }

        /// <summary>Creates a Style instance.</summary>
        /// <param name="id">The ID.</param>
        public Style(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ImpartError("ID cannot be null!");
            }
            _ID = id;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return "";
        }

        /// <summary>Dispose of the Style instance.</summary>
        public void Dispose() { }
    }
}