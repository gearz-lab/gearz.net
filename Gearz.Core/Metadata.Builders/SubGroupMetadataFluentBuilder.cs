using JetBrains.Annotations;

namespace Gearz.Core.Metadata.Builders
{
    public sealed class SubGroupMetadataFluentBuilder<T, TParentUIContext> : GroupMetadataFluentBuilder<T, TParentUIContext, SubGroupMetadataFluentBuilder<T, TParentUIContext>>,
        IGroupItemMetadataFluentBuilderEx<T, TParentUIContext, SubGroupMetadataFluentBuilder<T, TParentUIContext>>
        where TParentUIContext : IUIContext
    {
        public SubGroupMetadataFluentBuilder([NotNull] SubGroupMetadataBuilder<T, TParentUIContext> inner)
            : base(inner)
        {
        }
    }
}