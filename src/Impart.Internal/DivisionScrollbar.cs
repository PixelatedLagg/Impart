using System.Text;

namespace Impart.Internal
{
    public class DivisionScrollbar : StyleElement
    {
        private Axis Axis;
        private Length Width;
        private Color BackgroundColor;
        private Color ForegroundColor;
        private Length Radius = null;
        private string ID;

        /// <value>The ID value of the DivisionScrollbar. (always returns null)</value>
        string StyleElement.ID
        {
            get
            {
                return null;
            }
            set { }
        }
        private int _IOID;

        /// <value>The internal ID of the instance.</value>
        int StyleElement.IOID
        {
            get
            {
                return _IOID;
            }
        }

        public DivisionScrollbar(Scrollbar scrollbar, string id, int ioid)
        {
            Axis = scrollbar.Axis;
            Width = scrollbar.Width;
            BackgroundColor = scrollbar.BackgroundColor;
            ForegroundColor = scrollbar.ForegroundColor;
            Radius = scrollbar.Radius;
            ID = id;
            _IOID = ioid;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($"#{ID}::-webkit-scrollbar {{width: {Width};background-color: #808080; }}#{ID}::-webkit-scrollbar-track{{background-color: {BackgroundColor};}}#{ID}::-webkit-scrollbar-thumb {{background-color: {ForegroundColor};");
            if (Radius != null)
            {
                result.Append($"border-radius: {Radius};}}");
            }
            else
            {
                result.Append('}');
            }
            return result.ToString();
        }
    }
}