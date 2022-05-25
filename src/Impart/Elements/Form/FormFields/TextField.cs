using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Text field for Form.</summary>
    public sealed class TextField : FormField
    {
        private string _ID;
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        private string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }
        internal int FormID;
        private bool Changed = true;
        private string Render;

        /// <summary>Creates a TextField instance.</summary>
        /// <param name="text">The TextField text.</param>
        public TextField(string text)
        {
            Text = text;
            ID = null;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed)
            {
                return Render;
            }
            Changed = false;
            StringBuilder result = new StringBuilder("<label for=\"{FormID}\"");
            
            Render = $"<label for=\"{FormID}\">{Text}</label><input type=\"text\" name=\"{FormID}\">";
            return Render;
        }
    }
}