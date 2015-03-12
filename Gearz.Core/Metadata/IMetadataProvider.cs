namespace Gearz.Core.Metadata
{
    /// <summary>
    /// Represents a class able to provide metadata to a Gearz application.
    /// </summary>
    public interface IMetadataProvider
    {
        /// <summary>
        /// Sets up the application metadata using the given metadata context.
        /// </summary>
        /// <param name="context">The metadata context to setup.</param>
        void SetupMetadata(MetadataContext context);
    }
}