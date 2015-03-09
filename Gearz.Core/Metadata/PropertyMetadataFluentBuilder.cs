using JetBrains.Annotations;

namespace Gearz.Core.Metadata
{
    public class PropertyMetadataFluentBuilder<TProp, TParentUIContext> : MetadataFluentBuilder<TProp, TParentUIContext, PropertyMetadataFluentBuilder<TProp, TParentUIContext>>,
        IGroupItemMetadataFluentBuilder<TProp, TParentUIContext, PropertyMetadataFluentBuilder<TProp, TParentUIContext>>
        where TParentUIContext : UIContext
    {
        private readonly PropertyMetadataBuilder<TProp, TParentUIContext> inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetadataFluentBuilder{TProp,TParentUIContext}"/> class.
        /// </summary>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public PropertyMetadataFluentBuilder([NotNull] PropertyMetadataBuilder<TProp, TParentUIContext> inner)
            : base(inner)
        {
            this.inner = inner;
        }
    }
}