namespace Impart
{
    /// <summary>Element interface.</summary>
    public interface Element 
    {
        public int IOID { get; }
        public string ID { get; set; }
    }

    internal interface Nested 
    {
        internal string First();
        internal string Last();
    }
}