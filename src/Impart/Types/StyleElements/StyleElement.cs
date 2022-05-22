namespace Impart
{
    /// <summary>StyleElement interface.</summary>
    public interface StyleElement
    {
        /// <value>The Impart Object ID for the StyleElement.</value>
        int IOID { get; }

        /// <value>The ID for the StyleElement.</value>
        string ID { get; set; }
    }
}