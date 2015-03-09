using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Gearz.Core.Metadata
{
    public abstract class GroupMetadataFluentBuilder<T, TParentUIContext, TSelf> : MetadataFluentBuilder<T, TParentUIContext, TSelf>,
        IGroupMetadataFluentBuilder<T, TParentUIContext, TSelf>
        where TParentUIContext : UIContext
        where TSelf : class
    {
        [NotNull]
        private readonly IGroupMetadataBuilder<T, TParentUIContext> inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupMetadataFluentBuilder{T,TParentUIContext,TSelf}"/> class.
        /// </summary>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public GroupMetadataFluentBuilder([NotNull] IGroupMetadataBuilder<T, TParentUIContext> inner)
            : base(inner)
        {
            if (inner == null)
                throw new ArgumentNullException("inner");

            this.inner = inner;
        }

        /// <summary>
        /// Includes a group template to metadata as defaults.
        /// </summary>
        /// <param name="groupTypeName">Name of the group template to use.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf Template(string groupTypeName)
        {
            this.inner.Template(groupTypeName);
            return this as TSelf;
        }

        /// <summary>
        /// Includes a group template to metadata as defaults.
        /// </summary>
        /// <param name="groupType">A group template used as a pre-configuration of the group being created.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf Template(GroupTypeMetadataBuilder groupType)
        {
            this.inner.Template(groupType);
            return this as TSelf;
        }

        /// <summary>
        /// Indicates that a property participates in the view directly.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="expressionProperty">A lambda expression tree pointing to the property that appears in the view.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf Property<TProp>(
            Expression<Func<T, TProp>> expressionProperty)
        {
            this.inner.Property(expressionProperty);
            return this as TSelf;
        }

        /// <summary>
        /// Indicates that a property participates in the view,
        /// and that the property is configured with the passed delegate (in a fluent coding style).
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="expressionProperty">A lambda expression tree pointing to the property that appears in the view.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf Property<TProp>(
            Expression<Func<T, TProp>> expressionProperty,
            Action<PropertyMetadataFluentBuilder<TProp, UIContext<TProp, TParentUIContext>>> actionRegisterProp)
        {
            this.inner.Property(expressionProperty, actionRegisterProp);
            return this as TSelf;
        }

        /// <summary>
        /// Indicates that a property participates in the view,
        /// and that the property is configured with the passed delegate (in a fluent coding style).
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="propertyName">The name (or text expression) that identifies what property appears in the view.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf Property<TProp>(
            string propertyName,
            Action<PropertyMetadataFluentBuilder<TProp, UIContext<TProp, TParentUIContext>>> actionRegisterProp)
        {
            this.inner.Property(propertyName, actionRegisterProp);
            return this as TSelf;
        }

        /// <summary>
        /// Indicates that a property participates in the view.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="propertyName">The name (or text expression) that identifies what property appears in the view.</param>
        /// <param name="virtualProperty">An output object that can be used to refer to a virtual property when needed in further expression trees.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf Property<TProp>(
            string propertyName,
            out VirtualProperty<TProp> virtualProperty,
            Action<PropertyMetadataFluentBuilder<TProp, UIContext<TProp, TParentUIContext>>> actionRegisterProp)
        {
            this.inner.Property(propertyName, out virtualProperty, actionRegisterProp);
            return this as TSelf;
        }

        /// <summary>
        /// Includes a group of properties in the view.
        /// </summary>
        /// <param name="groupName">Name of the group that is being included in the view.</param>
        /// <param name="actionRegisterGroup">A delegate that is used to setup the group metadata.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf Group(string groupName, Action<SubGroupMetadataFluentBuilder<T, TParentUIContext>> actionRegisterGroup)
        {
            this.inner.Group(groupName, actionRegisterGroup);
            return this as TSelf;
        }
    }
}