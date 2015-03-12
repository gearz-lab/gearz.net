using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Gearz.Core.Metadata.Builders
{
    /// <summary>
    /// Metadata builder for groups allowing a fluent coding style.
    /// </summary>
    /// <typeparam name="T"> The type of the object that is in scope. </typeparam>
    /// <typeparam name="TParentUIContext"> The type of the parent UI scope object. </typeparam>
    /// <typeparam name="TSelf"> The type of the fluent metadata builder. </typeparam>
    public abstract class GroupMetadataFluentBuilder<T, TParentUIContext, TSelf> : MetadataFluentBuilder<T, TParentUIContext, TSelf>,
        IGroupMetadataFluentBuilderEx<T, TParentUIContext, TSelf>,
        INamedGroupOrTemplate
        where TParentUIContext : IUIContext
        where TSelf : GroupMetadataFluentBuilder<T, TParentUIContext, TSelf>
    {
        [NotNull]
        private readonly IGroupMetadataBuilderEx<T, TParentUIContext> inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupMetadataFluentBuilder{T,TParentUIContext,TSelf}"/> class.
        /// </summary>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public GroupMetadataFluentBuilder([NotNull] IGroupMetadataBuilderEx<T, TParentUIContext> inner)
            : base(inner)
        {
            if (inner == null)
                throw new ArgumentNullException("inner");

            this.inner = inner;
        }

        /// <summary>
        /// Includes a group template to metadata as defaults.
        /// </summary>
        /// <param name="templateName">Name of the entity to use as template.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf UseTemplate(string templateName)
        {
            this.inner.Template(templateName);
            return this as TSelf;
        }

        /// <summary>
        /// Includes a group template to metadata as defaults.
        /// </summary>
        /// <param name="template">A group template used as a pre-configuration of the group being created.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf UseTemplate(INamedGroupOrTemplate template)
        {
            this.inner.Template(template);
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
            Action<IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>> actionRegisterProp)
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
            Action<IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>> actionRegisterProp)
        {
            this.inner.Property(propertyName, actionRegisterProp);
            return this as TSelf;
        }

        /// <summary>
        /// Indicates that a property participates in the view,
        /// and that the property is configured with the passed delegate (in a fluent coding style).
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="propertyName">The name (or text expression) that identifies what property appears in the view.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public TSelf Property<TProp>(string propertyName)
        {
            this.inner.Property<TProp>(propertyName);
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
            Action<IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>> actionRegisterProp)
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

        string INamedGroupOrTemplate.Name
        {
            get { return this.inner.Name; }
        }
    }
}