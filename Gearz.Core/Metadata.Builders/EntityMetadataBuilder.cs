using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

namespace Gearz.Core.Metadata.Builders
{
    public class EntityMetadataBuilder<T> : GroupMetadataBuilder<T, UIContext<T, RootUIContext>>,
        IEntityMetadataBuilder
    {
        public EntityMetadataBuilder([NotNull] MetadataContext context, string name)
            : base(context, name)
        {
        }

        public EntityViewMetadataJsonModel GetJsonModel()
        {
            var items = this.ViewItems.ToImmutableArray();
            var templates = this.Templates.ToImmutableArray();
            var editor = this.EditorNames.SingleOrDefault();
            return new EntityViewMetadataJsonModel(items, templates, editor);
        }
    }
}
