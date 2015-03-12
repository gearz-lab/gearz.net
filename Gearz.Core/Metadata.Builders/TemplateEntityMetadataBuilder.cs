using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;

namespace Gearz.Core.Metadata.Builders
{
    public class TemplateEntityMetadataBuilder<T> : GroupMetadataBuilder<T, UIContext<T, UnknownUIContext>>,
        IEntityMetadataBuilder
    {
        public TemplateEntityMetadataBuilder([NotNull] MetadataContext context, string name)
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