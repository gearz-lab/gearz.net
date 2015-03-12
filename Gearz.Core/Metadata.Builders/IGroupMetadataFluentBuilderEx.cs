using System;
using System.Linq.Expressions;

namespace Gearz.Core.Metadata.Builders
{
    public interface IGroupMetadataFluentBuilderEx<T, TParentUIContext, out TSelf> :
        IGroupMetadataFluentBuilder<TParentUIContext, TSelf>
        where TParentUIContext : IUIContext
        where TSelf : IGroupMetadataFluentBuilderEx<T, TParentUIContext, TSelf>
    {
        /// <summary>
        /// Indicates that a property participates in the view directly.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="expressionProperty">A lambda expression tree pointing to the property that appears in the view.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf Property<TProp>(
            Expression<Func<T, TProp>> expressionProperty);

        /// <summary>
        /// Indicates that a property participates in the view,
        /// and that the property is configured with the passed delegate (in a fluent coding style).
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="expressionProperty">A lambda expression tree pointing to the property that appears in the view.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf Property<TProp>(
            Expression<Func<T, TProp>> expressionProperty,
            Action<IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>> actionRegisterProp);

        /// <summary>
        /// Includes a group of properties in the view.
        /// </summary>
        /// <param name="groupName">Name of the group that is being included in the view.</param>
        /// <param name="actionRegisterGroup">A delegate that is used to setup the group metadata.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf Group(string groupName, Action<SubGroupMetadataFluentBuilder<T, TParentUIContext>> actionRegisterGroup);
    }

    public interface IGroupMetadataFluentBuilder<in TParentUIContext, out TSelf>
        where TParentUIContext : IUIContext
        where TSelf : IGroupMetadataFluentBuilder<TParentUIContext, TSelf>
    {
        /// <summary>
        /// Includes a group template to metadata as defaults.
        /// </summary>
        /// <param name="templateName">Name of the group template to use.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf UseTemplate(string templateName);

        /// <summary>
        /// Includes a group template to metadata as defaults.
        /// </summary>
        /// <param name="template">A group template used as a pre-configuration of the group being created.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf UseTemplate(INamedGroupOrTemplate template);

        /// <summary>
        /// Indicates that a property participates in the view,
        /// and that the property is configured with the passed delegate (in a fluent coding style).
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="propertyName">The name (or text expression) that identifies what property appears in the view.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf Property<TProp>(
            string propertyName,
            Action<IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>> actionRegisterProp);

        /// <summary>
        /// Indicates that a property participates in the view.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="propertyName">The name (or text expression) that identifies what property appears in the view.</param>
        /// <param name="virtualProperty">An output object that can be used to refer to a virtual property when needed in further expression trees.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        TSelf Property<TProp>(
            string propertyName,
            out VirtualProperty<TProp> virtualProperty,
            Action<IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>> actionRegisterProp);
    }
}