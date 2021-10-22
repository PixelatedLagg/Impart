namespace Csweb
{
    public class Image
    {
        private string _path;
        private string _id;
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
        public Image(string path, string id = null)
        {
            this._path = path;
            this._id = id;
        }
        
    } 
}