using System.Text;

namespace Impart.API
{
    public struct JsonArray
    {
        private string title;
        private StringBuilder builder;
        private int _length;
        public int length
        {
            get
            {
                return _length;
            }
        }
        public JsonArray(string title, params (object, object)[] entries)
        {
            this.title = title;
            builder = new StringBuilder();
            _length = 0;
            for (int i = 0; i < entries.Length; i++)
            {
                if (i == 0)
                {
                    builder.Append($"\"{entries[i].Item1}\" : \"{entries[i].Item2}\"");
                }
                else
                {
                    builder.Append($", \"{entries[i].Item1}\" : \"{entries[i].Item2}\"");
                }
                _length++;
            }
        }
        public JsonArray AddSet(object name, object value)
        {
            if (builder.Length == 0)
            {
                builder.Append($"\"{name}\" : \"{value}\"");
            }
            else
            {
                builder.Append($", \"{name}\" : \"{value}\"");
            }
            _length++;
            return this;
        }
        public JsonArray AddArray(JsonArray array)
        {
            if (builder.Length == 0)
            {
                builder.Append(array.Render());
            }
            else
            {
                string str = array.Render();
                builder.Append($", {str}");
            }
            _length++;
            return this;
        }
        internal string Render()
        {
            return $"\"{title}\" : {{ {builder.ToString()} }}";
        }
    }
}