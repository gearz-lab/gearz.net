using JetBrains.Annotations;

namespace Gearz.Core.Metadata
{
    public sealed class SubGroupMetadataFluentBuilder<T, TParentUIContext> : GroupMetadataFluentBuilder<T, TParentUIContext, SubGroupMetadataFluentBuilder<T, TParentUIContext>>,
        IGroupItemMetadataFluentBuilder<T, TParentUIContext, SubGroupMetadataFluentBuilder<T, TParentUIContext>>
        where TParentUIContext : UIContext
    {
        public SubGroupMetadataFluentBuilder([NotNull] SubGroupMetadataBuilder<T, TParentUIContext> inner)
            : base(inner)
        {
        }
    }
}