using JetBrains.Annotations;

namespace Gearz.Core.Metadata.Builders
{
    public class PropertyMetadataFluentBuilder<TProp, TParentUIContext> : MetadataFluentBuilder<TProp, TParentUIContext, PropertyMetadataFluentBuilder<TProp, TParentUIContext>>,
        IPropertyMetadataFluentBuilder<TProp, TParentUIContext>,
        IGroupItemMetadataFluentBuilderEx<TProp, TParentUIContext, PropertyMetadataFluentBuilder<TProp, TParentUIContext>>
        where TParentUIContext : IUIContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetadataFluentBuilder{TProp,TParentUIContext}"/> class.
        /// </summary>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public PropertyMetadataFluentBuilder([NotNull] PropertyMetadataBuilder<TProp, TParentUIContext> inner)
            : base(inner)
        {
        }

        IPropertyMetadataFluentBuilder<TProp, TParentUIContext> IMetadataFluentBuilder<IPropertyMetadataFluentBuilder<TProp, TParentUIContext>>.Display(string text)
        {
            return this.Display(text);
        }

        IPropertyMetadataFluentBuilder<TProp, TParentUIContext> IMetadataFluentBuilder<IPropertyMetadataFluentBuilder<TProp, TParentUIContext>>.Editor(string editorName)
        {
            return this.Editor(editorName);
        }

        IPropertyMetadataFluentBuilder<TProp, TParentUIContext> IMetadataFluentBuilder<IPropertyMetadataFluentBuilder<TProp, TParentUIContext>>.Hint(string hintName, object value)
        {
            return this.Hint(hintName, value);
        }
    }

    public interface IPropertyMetadataFluentBuilder<in TProp, in TParentUIContext> :
        IGroupItemMetadataFluentBuilder<IPropertyMetadataFluentBuilder<TProp, TParentUIContext>>
        where TParentUIContext : IUIContext
    {
    }
}