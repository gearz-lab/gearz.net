using System;
using System.Linq.Expressions;

namespace Gearz.Core.Metadata
{
    public interface IGroupMetadataBuilder<T, TSelf>
    {
        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// This is a final value, since it is a constant.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf Display(string text);

        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// When multiple are added, the first one that can build a valid string is used.
        /// A valid string is non-null nor empty string, without throwing errors.
        /// </summary>
        /// <param name="textBuilderExpression">Lambda expression that can build the text to display.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf Display(Expression<Func<UIContext<T>, string>> textBuilderExpression);

        /// <summary>
        /// Includes an editor name that can be used with this entity type.
        /// If more than one is added, then the UI builder will choose the alternative that best suits,
        /// based on the affinity of the component with the context where it is going to be inserted.
        /// </summary>
        /// <param name="editorName">The editor component name, used to edit this entity.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf Editor(string editorName);

        TSelf Hint(string hintName, object value);

        TSelf Property<TProp>(
            Expression<Func<T, TProp>> actionRegisterProp);

        TSelf Property<TProp>(
            Expression<Func<T, TProp>> expressionProperty,
            Action<PropertyMetadataBuilder<T, TProp>> actionRegisterProp);

        TSelf Property<TProp>(
            string propertyName,
            Action<PropertyMetadataBuilder<T, TProp>> actionRegisterProp);

        TSelf Property<TProp>(
            string propertyName,
            out VirtualProperty<TProp> virtualProperty,
            Action<PropertyMetadataBuilder<T, TProp>> actionRegisterProp);

        TSelf Group(string groupTypeName, string groupName, Action<TSelf> func1);

        TSelf Group(GroupTypeMetadataBuilder groupType, string groupName, Action<TSelf> func1);
    }

    public class UIContext<T>
    {
        public T Value { get; set; }
    }
}