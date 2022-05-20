namespace Impart
{
    /// <summary>Element interface.</summary>
    public interface Element 
    {
        /// <value>IOID value for the Element.</value>
        int IOID { get; }

        /// <value>ID value for the Element.</value>
        string ID { get; set; }

        /// <summary>Clones the Element (including its IOID).</summary>
        Element Clone();
    }
}