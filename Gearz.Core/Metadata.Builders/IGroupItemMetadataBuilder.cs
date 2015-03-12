namespace Gearz.Core.Metadata.Builders
{
    public interface IGroupItemMetadataBuilder<T, TParentUIContext> :
        IMetadataBuilderEx<T, TParentUIContext>
        where TParentUIContext : IUIContext
    {
    }
}