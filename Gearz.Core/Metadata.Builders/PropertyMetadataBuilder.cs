using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Gearz.Core.Metadata.Builders
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Classes have the same name.")]
    public abstract class PropertyMetadataBuilder
    {
        internal PropertyMetadataBuilder()
        {
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Classes have the same name.")]
    public class PropertyMetadataBuilder<TProp, TParentUIContext> : PropertyMetadataBuilder,
        IGroupItemMetadataBuilder<TProp, TParentUIContext>
        where TParentUIContext : IUIContext
    {
        private readonly List<string> editorNames = new List<string>();
        private readonly Dictionary<string, List<object>> hints = new Dictionary<string, List<object>>();

        private readonly List<Expression<Func<IUIContext<TProp, TParentUIContext>, string>>> displayNames
            = new List<Expression<Func<IUIContext<TProp, TParentUIContext>, string>>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetadataBuilder{TProp,TParentUIContext}"/> class.
        /// </summary>
        /// <param name="index"> The index of this property inside the parent group. </param>
        /// <param name="propertyName"> The property name of this property inside the parent group. </param>
        public PropertyMetadataBuilder(int index, [NotNull] string propertyName)
        {
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");
            this.Index = index;
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Gets the index of this property inside the parent group.
        /// </summary>
        [CanBeNull]
        [UsedImplicitly]
        public int? Index { get; private set; }

        /// <summary>
        /// Gets the property name of this property inside the parent group.
        /// </summary>
        [CanBeNull]
        [UsedImplicitly]
        public string PropertyName { get; private set; }

        /// <summary>
        /// Gets the metadata context in which metadata is being created.
        /// </summary>
        [NotNull]
        [UsedImplicitly]
        public MetadataContext Context { get; private set; }

        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// Accepts the use of contextual information in the string (e.g. "Title {ObjectName}").
        /// When multiple are added, the first one that can be used to build a valid string is used.
        /// A valid string is non-null nor empty string, without errors.
        /// </summary>
        /// <param name="text">Text to display.</param>
        public void Display(string text)
        {
            this.displayNames.Add(Expression.Lambda<Func<IUIContext<TProp, TParentUIContext>, string>>(
                Expression.Constant(text),
                Expression.Parameter(typeof(IUIContext<TProp, TParentUIContext>), "ctx")));
        }

        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// When multiple are added, the first one that can build a valid string is used.
        /// A valid string is non-null nor empty string, without throwing errors.
        /// </summary>
        /// <param name="textBuilderExpression">Lambda expression that can build the text to display.</param>
        public void Display(Expression<Func<IUIContext<TProp, TParentUIContext>, string>> textBuilderExpression)
        {
            this.displayNames.Add(textBuilderExpression);
        }

        /// <summary>
        /// Includes an editor name that can be used with this entity type.
        /// If more than one is added, then the UI builder will choose the alternative that best suits,
        /// based on the affinity of the component with the context where it is going to be inserted.
        /// </summary>
        /// <param name="editorName">The editor component name, used to edit this entity.</param>
        public void Editor(string editorName)
        {
            var idx = this.editorNames.BinarySearch(editorName);
            if (idx < 0)
                this.editorNames.Insert(~idx, editorName);
        }

        /// <summary>
        /// Includes a hint, in a named collection of hints.
        /// Multiple hints may be given for a single name.
        /// </summary>
        /// <param name="hintName">The name of the hint.</param>
        /// <param name="value">The value to add to the named hint collection.</param>
        public void Hint(string hintName, object value)
        {
            List<object> hintValueList;
            if (!this.hints.TryGetValue(hintName, out hintValueList))
                this.hints[hintName] = hintValueList = new List<object>();

            hintValueList.Add(value);
        }
    }
}