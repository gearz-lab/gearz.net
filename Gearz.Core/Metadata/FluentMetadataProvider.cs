﻿namespace Gearz.Core.Metadata
{
    /// <summary>
    /// MetadataProvider that uses a fluent API
    /// </summary>
    public abstract class FluentMetadataProvider : IMetadataProvider
    {
        /// <summary>
        /// Sets up the application metadata using the given metadata context
        /// </summary>
        /// <param name="context">The metadata context to be configured.</param>
        public abstract void SetupMetadata(MetadataContext context);
    }
}
