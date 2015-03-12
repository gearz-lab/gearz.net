namespace Gearz.Core.Metadata.Builders
{
    public class SubGroupMetadataBuilder<T, TParentUIContext> : GroupMetadataBuilder<T, TParentUIContext>,
        IGroupItemMetadataBuilder<T, TParentUIContext>
        where TParentUIContext : IUIContext
    {
        public SubGroupMetadataBuilder(MetadataContext context, int index, string groupName)
            : base(context, index, groupName)
        {
        }
    }
}