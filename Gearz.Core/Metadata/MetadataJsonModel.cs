using System.Collections.Immutable;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Gearz.Core.Metadata
{
    public class MetadataJsonModel
    {
        public MetadataJsonModel(
            ImmutableDictionary<string, EntityViewMetadataJsonModel> entities,
            ImmutableDictionary<string, EntityViewMetadataJsonModel> templates)
        {
            this.Entities = entities;
            this.Templates = templates;
        }

        [JsonProperty("entities")]
        [UsedImplicitly]
        public ImmutableDictionary<string, EntityViewMetadataJsonModel> Entities { get; private set; }

        [JsonProperty("templates")]
        [UsedImplicitly]
        public ImmutableDictionary<string, EntityViewMetadataJsonModel> Templates { get; private set; }
    }
}