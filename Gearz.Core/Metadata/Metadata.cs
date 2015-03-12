using System.Collections.Generic;

namespace Gearz.Core.Metadata
{
    public static class Metadata
    {
        private static readonly List<IMetadataProvider> metadataProviders = new List<IMetadataProvider>();

        public static void Register(IMetadataProvider metadataProvider)
        {
            lock (metadataProviders)
                metadataProviders.Add(metadataProvider);
        }

        public static object GetMetadata()
        {
            lock (metadataProviders)
            {
                var metadataContext = new MetadataContext();

                foreach (var metadataProvider in metadataProviders)
                    metadataProvider.SetupMetadata(metadataContext);

                var result = metadataContext.GetJsonModel();
                return result;
            }
        }
    }
}