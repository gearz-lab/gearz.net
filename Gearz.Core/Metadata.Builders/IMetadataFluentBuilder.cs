using System;
using System.Linq.Expressions;

namespace Gearz.Core.Metadata.Builders
{
    public interface IMetadataFluentBuilder<out TSelf>
        where TSelf : IMetadataFluentBuilder<TSelf>
    {
        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// Accepts the use of contextual information in the string (e.g. "Title {ObjectName}").
        /// When multiple are added, the first one that can be used to build a valid string is used.
        /// A valid string is non-null nor empty string, without errors.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf Display(string text);

        /// <summary>
        /// Includes an editor name that can be used with this entity type.
        /// If more than one is added, then the UI builder will choose the alternative that best suits,
        /// based on the affinity of the component with the context where it is going to be inserted.
        /// </summary>
        /// <param name="editorName">The editor component name, used to edit this entity.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf Editor(string editorName);

        /// <summary>
        /// Includes a hint, in a named collection of hints.
        /// Multiple hints may be given for a single name.
        /// </summary>
        /// <param name="hintName">The name of the hint.</param>
        /// <param name="value">The value to add to the named hint collection.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf Hint(string hintName, object value);
    }

    public interface IMetadataFluentBuilderEx<T, TParentUIContext, out TSelf> :
        IMetadataFluentBuilder<TSelf>
        where TParentUIContext : IUIContext
        where TSelf : IMetadataFluentBuilderEx<T, TParentUIContext, TSelf>
    {
        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// When multiple are added, the first one that can build a valid string is used.
        /// A valid string is non-null nor empty string, without throwing errors.
        /// </summary>
        /// <param name="textBuilderExpression">Lambda expression that can build the text to display.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf Display(Expression<Func<IUIContext<T, TParentUIContext>, string>> textBuilderExpression);
    }
}