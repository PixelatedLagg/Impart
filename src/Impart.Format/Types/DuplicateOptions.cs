namespace Impart.Format
{
    /// <summary>All duplicate options for JSON/XML.</summary>
    public enum DuplicateOptions
    {
        /// <value>Overwrite the first entry with the second in the dataset.</value>
        Overwrite,

        /// <value>Do not add the duplicate to the dataset.</value>
        Ignore,

        /// <value>Add the duplicate to the dataset.</value>
        Allow
    }
}