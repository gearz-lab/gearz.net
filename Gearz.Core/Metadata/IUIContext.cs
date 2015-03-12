namespace Gearz.Core.Metadata
{
    public interface IUIContext<out T, out TParentUIContext> :
        IUIContext
        where TParentUIContext : IUIContext
    {
        /// <summary>
        /// Gets the value of the current property.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Gets the parent context.
        /// </summary>
        TParentUIContext Parent { get; }
    }

    public interface IUIContext
    {
        /// <summary>
        /// Gets the name of the current property in the parent object.
        /// </summary>
        string Property { get; }
    }
}