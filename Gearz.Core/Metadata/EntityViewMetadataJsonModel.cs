using System.Collections.Immutable;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Gearz.Core.Metadata
{
    /// <summary>
    /// Represents metadata to be sent to the client in a JavaScript serialized code.
    /// </summary>
    public class EntityViewMetadataJsonModel
    {
        public EntityViewMetadataJsonModel(
            ImmutableArray<object> items,
            ImmutableArray<string> templates,
            string editor)
        {
            this.Items = items;
            this.Templates = templates;
            this.Editor = editor;
        }

        /// <summary>
        /// Gets the items contained in this group.
        /// </summary>
        [JsonProperty("items")]
        [UsedImplicitly]
        public ImmutableArray<object> Items { get; private set; }

        /// <summary>
        /// Gets the templates that this group inherits metadata from.
        /// </summary>
        [JsonProperty("templates")]
        [UsedImplicitly]
        public ImmutableArray<string> Templates { get; private set; }

        /// <summary>
        /// Gets the editors that can be used to edit this group.
        /// </summary>
        [JsonProperty("editor")]
        [UsedImplicitly]
        public string Editor { get; private set; }
    }
}