namespace Gearz.Core.Metadata
{
    public interface IGroupItemMetadataBuilder<T, TParentUIContext> :
        IMetadataBuilder<T, TParentUIContext>
        where TParentUIContext : UIContext
    {
    }
}