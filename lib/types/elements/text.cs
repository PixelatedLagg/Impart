namespace Csweb
{
    public class Text
    {
        private string _text;
        private string _id;
        public string text 
        {
            get 
            {
                return _text;
            }
        }
        public string id 
        {
            get 
            {
                return _id;
            }
        }
        public Text(string text, string id = null)
        {
            this._text = text;
            this._id = id;
        }
    } 
}