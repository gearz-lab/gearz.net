using JetBrains.Annotations;

namespace Gearz.Core.Metadata
{
    public sealed class EntityMetadataFluentBuilder<T> : GroupMetadataFluentBuilder<T, UIContext<T, RootUIContext>, SubGroupMetadataFluentBuilder<T, UIContext<T, RootUIContext>>>
    {
        public EntityMetadataFluentBuilder([NotNull] EntityMetadataBuilder<T> inner)
            : base(inner)
        {
        }
    }
}