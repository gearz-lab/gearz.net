using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Gearz.Core.Metadata.Builders
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Classes have the same name.")]
    public abstract class MetadataFluentBuilder<T, TParentUIContext, TSelf> : MetadataFluentBuilder<TParentUIContext, TSelf>,
        IMetadataFluentBuilderEx<T, TParentUIContext, TSelf>
        where TParentUIContext : IUIContext
        where TSelf : MetadataFluentBuilder<T, TParentUIContext, TSelf>
    {
        [NotNull]
        private readonly IMetadataBuilderEx<T, TParentUIContext> inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataFluentBuilder{T,TParentUIContext,TSelf}"/> class.
        /// </summary>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public MetadataFluentBuilder([NotNull] IMetadataBuilderEx<T, TParentUIContext> inner)
            : base(inner)
        {
            this.inner = inner;
        }

        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// When multiple are added, the first one that can build a valid string is used.
        /// A valid string is non-null nor empty string, without throwing errors.
        /// </summary>
        /// <param name="textBuilderExpression">Lambda expression that can build the text to display.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf Display(Expression<Func<IUIContext<T, TParentUIContext>, string>> textBuilderExpression)
        {
            this.inner.Display(textBuilderExpression);
            return this as TSelf;
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Classes have the same name.")]
    public abstract class MetadataFluentBuilder<TParentUIContext, TSelf> :
        IMetadataFluentBuilder<TSelf>
        where TParentUIContext : IUIContext
        where TSelf : MetadataFluentBuilder<TParentUIContext, TSelf>
    {
        [NotNull]
        private readonly IMetadataBuilder inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataFluentBuilder{TParentUIContext,TSelf}"/> class.
        /// </summary>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public MetadataFluentBuilder([NotNull] IMetadataBuilder inner)
        {
            if (inner == null)
                throw new ArgumentNullException("inner");

            this.inner = inner;
        }

        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// Accepts the use of contextual information in the string (e.g. "Title {ObjectName}").
        /// When multiple are added, the first one that can be used to build a valid string is used.
        /// A valid string is non-null nor empty string, without errors.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf Display(string text)
        {
            this.inner.Display(text);
            return this as TSelf;
        }

        /// <summary>
        /// Includes an editor name that can be used with this entity type.
        /// If more than one is added, then the UI builder will choose the alternative that best suits,
        /// based on the affinity of the component with the context where it is going to be inserted.
        /// </summary>
        /// <param name="editorName">The editor component name, used to edit this entity.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf Editor(string editorName)
        {
            this.inner.Editor(editorName);
            return this as TSelf;
        }

        /// <summary>
        /// Includes a hint, in a named collection of hints.
        /// Multiple hints may be given for a single name.
        /// </summary>
        /// <param name="hintName">The name of the hint.</param>
        /// <param name="value">The value to add to the named hint collection.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf Hint(string hintName, object value)
        {
            this.inner.Hint(hintName, value);
            return this as TSelf;
        }
    }
}