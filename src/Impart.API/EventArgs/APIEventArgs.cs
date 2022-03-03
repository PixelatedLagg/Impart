using System;

namespace Impart.API
{
    public struct APIEventArgs
    {
        public Request request;
        public DateTime time;
        public APIEventArgs(Request request)
        {
            this.request = request;
            time = DateTime.Now;
        }
    }
}