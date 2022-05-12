namespace Impart
{
    /// <summary>Element interface.</summary>
    public interface Element 
    {
        int IOID { get; }
        string ID { get; set; }
        Element Clone();
    }
}