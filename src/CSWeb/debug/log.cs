namespace CSWeb
{
    public struct Log
    {
        public double ms;
        public string log;
        public Log(string log, double ms)
        {
            this.log = log;
            this.ms = ms;
        }
    }
}