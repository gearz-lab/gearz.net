using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace Gearz.Core.Metadata.Builders
{
    [DebuggerDisplay("{GetType().Name}")]
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

        public override string ToString()
        {
            return base.ToString();
        }
    }
}