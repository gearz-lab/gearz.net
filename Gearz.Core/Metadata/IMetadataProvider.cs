namespace Gearz.Core.Metadata
{
    /// <summary>
    /// Represents a class able to provide matadata to a Gearz application
    /// </summary>
    public interface IMetadataProvider
    {
        /// <summary>
        /// Sets up the application metadata using the given metadata context
        /// </summary>
        /// <param name="context"></param>
        void SetupMetadata(MetadataContext context);
    }
}