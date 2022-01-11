using System;

namespace Impart
{
    public struct WebInfo
    {
        public DateTime localTime;
        public string url;
        public WebInfo(string url, double localTime)
        {
            this.url = url;
            this.localTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            this.localTime.AddSeconds(localTime).ToLocalTime();
        }
    }
}