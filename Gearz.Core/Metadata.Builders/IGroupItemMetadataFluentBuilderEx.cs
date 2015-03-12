namespace Gearz.Core.Metadata.Builders
{
    public interface IGroupItemMetadataFluentBuilderEx<T, TParentUIContext, out TSelf> :
        IMetadataFluentBuilderEx<T, TParentUIContext, TSelf>
        where TParentUIContext : IUIContext
        where TSelf : IMetadataFluentBuilderEx<T, TParentUIContext, TSelf>
    {
    }

    public interface IGroupItemMetadataFluentBuilder<out TSelf> :
        IMetadataFluentBuilder<TSelf>
        where TSelf : IGroupItemMetadataFluentBuilder<TSelf>
    {
    }
}