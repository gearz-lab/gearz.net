using System;
using System.Linq.Expressions;

namespace Gearz.Core.Metadata.Builders
{
    /// <summary>
    /// Represents the ability to build groups of properties in the view.
    /// </summary>
    /// <typeparam name="T">The type of the represented entity.</typeparam>
    /// <typeparam name="TParentUIContext">The type of the parent UI context.</typeparam>
    public interface IGroupMetadataBuilderEx<T, TParentUIContext> :
        IMetadataBuilderEx<T, TParentUIContext>,
        IGroupMetadataBuilder<TParentUIContext>
        where TParentUIContext : IUIContext
    {
        /// <summary>
        /// Indicates that a property participates in the view directly.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="expressionProperty">A lambda expression tree pointing to the property that appears in the view.</param>
        void Property<TProp>(
            Expression<Func<T, TProp>> expressionProperty);

        /// <summary>
        /// Indicates that a property participates in the view,
        /// and that the property is configured with the passed delegate (in a fluent coding style).
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="expressionProperty">A lambda expression tree pointing to the property that appears in the view.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        void Property<TProp>(
            Expression<Func<T, TProp>> expressionProperty,
            Action<IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>> actionRegisterProp);

        /// <summary>
        /// Includes a group of properties in the view.
        /// </summary>
        /// <param name="groupName">Name of the group that is being included in the view.</param>
        /// <param name="actionRegisterGroup">A delegate that is used to setup the group metadata.</param>
        void Group(string groupName, Action<SubGroupMetadataFluentBuilder<T, TParentUIContext>> actionRegisterGroup);
    }

    public interface IGroupMetadataBuilder<in TParentUIContext> :
        INamedGroupOrTemplate
        where TParentUIContext : IUIContext
    {
        /// <summary>
        /// Includes a group template to metadata as defaults.
        /// </summary>
        /// <param name="groupTypeName">Name of the group template to use.</param>
        void Template(string groupTypeName);

        /// <summary>
        /// Includes a group template to metadata as defaults.
        /// </summary>
        /// <param name="template">A group template used as a pre-configuration of the group being created.</param>
        void Template(INamedGroupOrTemplate template);

        /// <summary>
        /// Indicates that a property participates in the view,
        /// and that the property is configured with the passed delegate (in a fluent coding style).
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="propertyName">The name (or text expression) that identifies what property appears in the view.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        void Property<TProp>(
            string propertyName,
            Action<IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>> actionRegisterProp);

        /// <summary>
        /// Indicates that a property participates in the view,
        /// and that the property is configured with the passed delegate (in a fluent coding style).
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="propertyName">The name (or text expression) that identifies what property appears in the view.</param>
        void Property<TProp>(string propertyName);

        /// <summary>
        /// Indicates that a property participates in the view.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="propertyName">The name (or text expression) that identifies what property appears in the view.</param>
        /// <param name="virtualProperty">An output object that can be used to refer to a virtual property when needed in further expression trees.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        void Property<TProp>(
            string propertyName,
            out VirtualProperty<TProp> virtualProperty,
            Action<IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>> actionRegisterProp);
    }
}