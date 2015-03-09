namespace Gearz.Core.Metadata
{
    public interface IGroupItemMetadataFluentBuilder<T, TParentUIContext, out TSelf> :
        IMetadataFluentBuilder<T, TParentUIContext, TSelf>
        where TParentUIContext : UIContext
        where TSelf : class
    {
    }
}