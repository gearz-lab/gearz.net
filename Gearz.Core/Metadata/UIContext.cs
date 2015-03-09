namespace Gearz.Core.Metadata
{
    public class UIContext<T, TParentUIContext> : UIContext
        where TParentUIContext : UIContext
    {
        /// <summary>
        /// Gets the value of the current property.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Gets the name of the current property in the parent object.
        /// </summary>
        public string Property { get; private set; }

        /// <summary>
        /// Gets the parent context, if one exits.
        /// </summary>
        public TParentUIContext Parent { get; private set; }
    }

    public class UIContext
    {
    }

    public class RootUIContext : UIContext
    {
    }
}