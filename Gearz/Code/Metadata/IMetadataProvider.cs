using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Gearz.Code.Metadata
{
    /// <summary>
    /// Represents a class able to provide matadata to a Gearz application
    /// </summary>
    public interface IMetadataProvider
    {
        /// <summary>
        /// Sets up the application metadata using the given metadata context
        /// </summary>
        /// <param name="meta"></param>
        void SetupMetadata(MetadataContext meta);
    }
}