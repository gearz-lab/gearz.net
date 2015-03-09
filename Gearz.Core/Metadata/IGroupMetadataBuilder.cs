using System;
using System.Linq.Expressions;

namespace Gearz.Core.Metadata
{
    /// <summary>
    /// Represents the ability to build groups of properties in the view.
    /// </summary>
    /// <typeparam name="T">The type of the represented entity.</typeparam>
    public interface IGroupMetadataBuilder<T, TParentUIContext> :
        IMetadataBuilder<T, TParentUIContext>
        where TParentUIContext : UIContext
    {
        /// <summary>
        /// Includes a group template to metadata as defaults.
        /// </summary>
        /// <param name="groupTypeName">Name of the group template to use.</param>
        void Template(string groupTypeName);

        /// <summary>
        /// Includes a group template to metadata as defaults.
        /// </summary>
        /// <param name="groupType">A group template used as a pre-configuration of the group being created.</param>
        void Template(GroupTypeMetadataBuilder groupType);

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
            Action<PropertyMetadataFluentBuilder<TProp, UIContext<TProp, TParentUIContext>>> actionRegisterProp);

        /// <summary>
        /// Indicates that a property participates in the view,
        /// and that the property is configured with the passed delegate (in a fluent coding style).
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="propertyName">The name (or text expression) that identifies what property appears in the view.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        void Property<TProp>(
            string propertyName,
            Action<PropertyMetadataFluentBuilder<TProp, UIContext<TProp, TParentUIContext>>> actionRegisterProp);

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
            Action<PropertyMetadataFluentBuilder<TProp, UIContext<TProp, TParentUIContext>>> actionRegisterProp);

        /// <summary>
        /// Includes a group of properties in the view.
        /// </summary>
        /// <param name="groupName">Name of the group that is being included in the view.</param>
        /// <param name="actionRegisterGroup">A delegate that is used to setup the group metadata.</param>
        void Group(string groupName, Action<SubGroupMetadataFluentBuilder<T, TParentUIContext>> actionRegisterGroup);
    }
}