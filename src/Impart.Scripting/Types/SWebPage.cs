using System;

namespace Impart.Scripting
{
    public sealed class SWebPage
    {
        private string file;
        public Action<WebInfo> OnClick;
        public Action<WebInfo> OffClick;
        public SWebPage(string file)
        {
            this.file = file;
        }
        public void Render()
        {
            //render
        }
    }
}