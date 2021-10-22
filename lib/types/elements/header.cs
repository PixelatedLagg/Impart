namespace Csweb
{
    public class Header
    {
        private string _text;
        private string _id;
        internal int num;
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
        public Header(int num, string path, string id = null)
        {
            this._text = path;
            this._id = id;
            this.num = num;
        }
        
    } 
}