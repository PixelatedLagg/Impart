using System.Threading;
using System.Globalization;
using System;

namespace Csweb
{
    public class cswebobj
    {
        private string source;
        public string id;
        public cswebobj(cswebroot root, string id)
        {
            source = root.source;
            this.id = id;
        }
        public void AddComponent(int component, string name, string style)
        {
            switch (component)
            {
                case 0:
                //text
                break;
                case 1:
                //image
                break;
            }
        }
        public void Render()
        {
            //render all components
        }
    }
}