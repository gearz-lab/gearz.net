using JetBrains.Annotations;

namespace Gearz.Core.Metadata.Builders
{
    public sealed class EntityMetadataFluentBuilder<T> : GroupMetadataFluentBuilder<T, UIContext<T, RootUIContext>, EntityMetadataFluentBuilder<T>>
    {
        public EntityMetadataFluentBuilder([NotNull] EntityMetadataBuilder<T> inner)
            : base(inner)
        {
        }
    }
}