namespace Csweb
{
    public class Link
    {
        private Text _text;
        private Image _image;
        private string _path;
        private string _id;
        public Text text 
        {
            get 
            {
                return _text;
            }
        }
        public Image image 
        {
            get 
            {
                return _image;
            }
        }
        public string path 
        {
            get 
            {
                return _path;
            }
        }
        public string id 
        {
            get 
            {
                return _id;
            }
        }
        public Link(Text text, string path, string id = null)
        {
            this._text = text;
            this._id = id;
            this._path = path;
            this._image = null;
        }
        public Link(Image image, string path, string id = null)
        {
            this._text = null;
            this._id = id;
            this._path = path;
            this._image = image;
        }
    } 
}