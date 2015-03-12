using JetBrains.Annotations;

namespace Gearz.Core.Metadata.Builders
{
    public sealed class TemplateEntityMetadataFluentBuilder<T> : GroupMetadataFluentBuilder<T, UIContext<T, UnknownUIContext>, TemplateEntityMetadataFluentBuilder<T>>
    {
        public TemplateEntityMetadataFluentBuilder([NotNull] TemplateEntityMetadataBuilder<T> inner)
            : base(inner)
        {
        }
    }
}