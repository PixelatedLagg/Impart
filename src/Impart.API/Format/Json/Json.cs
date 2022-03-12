using System.Text;

namespace Impart.API
{
    public struct Json : Format
    {
        private StringBuilder builder;
        private string title;
        private int _length;
        public int length
        {
            get
            {
                return _length;
            }
        }
        public Json(string title, params (object, object)[] entries)
        {
            builder = new StringBuilder();
            this.title = title;
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
        public Json AddSet(object name, object value)
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
        public Json AddArray(JsonArray array)
        {
            if (builder.Length == 0)
            {
                builder.Append(array.Render());
            }
            else
            {
                builder.Append($", {array.Render()}");
            }
            _length++;
            return this;
        }
        internal string Render()
        {
            return $"{{ \"{title}\" : {{ {builder.ToString()} }} }}";
        }
    }
}