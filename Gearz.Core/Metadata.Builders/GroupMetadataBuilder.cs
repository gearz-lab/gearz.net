using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Gearz.Core.Helpers;
using JetBrains.Annotations;

namespace Gearz.Core.Metadata.Builders
{
    public abstract class GroupMetadataBuilder<T, TParentUIContext> :
        IGroupMetadataBuilderEx<T, TParentUIContext>
        where TParentUIContext : IUIContext
    {
        private readonly Dictionary<string, SubGroupMetadataBuilder<T, TParentUIContext>> groups
            = new Dictionary<string, SubGroupMetadataBuilder<T, TParentUIContext>>();

        private readonly Dictionary<string, PropertyMetadataBuilder> properties
            = new Dictionary<string, PropertyMetadataBuilder>();

        protected readonly List<Expression<Func<IUIContext<T, TParentUIContext>, string>>> DisplayNames
            = new List<Expression<Func<IUIContext<T, TParentUIContext>, string>>>();

        protected readonly List<object> ViewItems
            = new List<object>();

        protected readonly List<string> Templates
            = new List<string>();

        protected readonly List<string> EditorNames
            = new List<string>();

        protected readonly Dictionary<string, List<object>> Hints
            = new Dictionary<string, List<object>>();

        /// <summary>
        /// Gets the index of this group inside the parent group.
        /// </summary>
        [CanBeNull]
        [UsedImplicitly]
        public int? Index { get; private set; }

        /// <summary>
        /// Gets the group name of this group inside the parent group.
        /// </summary>
        [CanBeNull]
        [UsedImplicitly]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the metadata context in which metadata is being created.
        /// </summary>
        [NotNull]
        [UsedImplicitly]
        public MetadataContext Context { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupMetadataBuilder{T,TParentUIContext}"/> class.
        /// </summary>
        /// <param name="context"> The context. </param>
        /// <param name="name">The name of the group or template.</param>
        public GroupMetadataBuilder([NotNull] MetadataContext context, string name)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.Context = context;
            this.Name = name;
        }

        protected GroupMetadataBuilder([NotNull] MetadataContext context, [CanBeNull] int? index, [NotNull] string groupName)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (groupName == null)
                throw new ArgumentNullException("groupName");

            this.Context = context;
            this.Index = index;
            this.Name = groupName;
        }

        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// Accepts the use of contextual information in the string (e.g. "Title {ObjectName}").
        /// When multiple are added, the first one that can be used to build a valid string is used.
        /// A valid string is non-null nor empty string, without errors.
        /// </summary>
        /// <param name="text">Text to display.</param>
        public void Display(string text)
        {
            this.DisplayNames.Add(
                Expression.Lambda<Func<IUIContext<T, TParentUIContext>, string>>(
                    Expression.Constant(text),
                    Expression.Parameter(typeof(IUIContext<T, TParentUIContext>), "ctx")));
        }

        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// When multiple are added, the first one that can build a valid string is used.
        /// A valid string is non-null nor empty string, without throwing errors.
        /// </summary>
        /// <param name="textBuilderExpression">Lambda expression that can build the text to display.</param>
        public void Display(Expression<Func<IUIContext<T, TParentUIContext>, string>> textBuilderExpression)
        {
            this.DisplayNames.Add(textBuilderExpression);
        }

        /// <summary>
        /// Includes an editor name that can be used with this entity type.
        /// If more than one is added, then the UI builder will choose the alternative that best suits,
        /// based on the affinity of the component with the context where it is going to be inserted.
        /// </summary>
        /// <param name="editorName">The editor component name, used to edit this entity.</param>
        public void Editor(string editorName)
        {
            var idx = this.EditorNames.BinarySearch(editorName);
            if (idx < 0)
                this.EditorNames.Insert(~idx, editorName);
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
            if (!this.Hints.TryGetValue(hintName, out hintValueList))
                this.Hints[hintName] = hintValueList = new List<object>();

            hintValueList.Add(value);
        }

        /// <summary>
        /// Indicates that a property participates in the view,
        /// and that the property is configured with the passed delegate (in a fluent coding style).
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="propertyName">The name (or text expression) that identifies what property appears in the view.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        public void Property<TProp>(
            string propertyName,
            Action<IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>> actionRegisterProp)
        {
            var index = this.ViewItems.Count;

            // getting the property with that name
            PropertyMetadataBuilder propMeta;
            if (!this.properties.TryGetValue(propertyName, out propMeta))
            {
                this.properties[propertyName] = propMeta = new PropertyMetadataBuilder<TProp, IUIContext<TProp, TParentUIContext>>(index, propertyName);
                this.ViewItems.Add(propMeta);
            }

            actionRegisterProp(
                new PropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>(
                    (PropertyMetadataBuilder<TProp, IUIContext<TProp, TParentUIContext>>)propMeta));
        }

        /// <summary>
        /// Indicates that a property participates in the view,
        /// and that the property is configured with the passed delegate (in a fluent coding style).
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="propertyName">The name (or text expression) that identifies what property appears in the view.</param>
        public void Property<TProp>(string propertyName)
        {
            var index = this.ViewItems.Count;

            // getting the property with that name
            PropertyMetadataBuilder propMeta;
            if (!this.properties.TryGetValue(propertyName, out propMeta))
            {
                this.properties[propertyName] = propMeta = new PropertyMetadataBuilder<TProp, IUIContext<TProp, TParentUIContext>>(index, propertyName);
                this.ViewItems.Add(propMeta);
            }
        }

        /// <summary>
        /// Indicates that a property participates in the view.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="propertyName">The name (or text expression) that identifies what property appears in the view.</param>
        /// <param name="virtualProperty">An output object that can be used to refer to a virtual property when needed in further expression trees.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        public void Property<TProp>(
            string propertyName,
            out VirtualProperty<TProp> virtualProperty,
            Action<IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>> actionRegisterProp)
        {
            var index = this.ViewItems.Count;

            // getting the property with that name
            PropertyMetadataBuilder propMeta;
            if (!this.properties.TryGetValue(propertyName, out propMeta))
            {
                this.properties[propertyName] = propMeta = new PropertyMetadataBuilder<TProp, IUIContext<TProp, TParentUIContext>>(index, propertyName);
                this.ViewItems.Add(propMeta);
            }

            actionRegisterProp(
                new PropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>(
                    (PropertyMetadataBuilder<TProp, IUIContext<TProp, TParentUIContext>>)propMeta));

            virtualProperty = new VirtualProperty<TProp>(propertyName);
        }

        /// <summary>
        /// Indicates that a property participates in the view directly.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="expressionProperty">A lambda expression tree pointing to the property that appears in the view.</param>
        public void Property<TProp>(Expression<Func<T, TProp>> expressionProperty)
        {
            // getting the property name
            var propertyName = ExpressionHelper.GetPropertyName(expressionProperty);
            var index = this.ViewItems.Count;

            // getting the property with that name
            PropertyMetadataBuilder propMeta;
            if (!this.properties.TryGetValue(propertyName, out propMeta))
            {
                this.properties[propertyName] = propMeta = new PropertyMetadataBuilder<TProp, TParentUIContext>(index, propertyName);
                this.ViewItems.Add(propMeta);
            }
        }

        /// <summary>
        /// Indicates that a property participates in the view,
        /// and that the property is configured with the passed delegate (in a fluent coding style).
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="expressionProperty">A lambda expression tree pointing to the property that appears in the view.</param>
        /// <param name="actionRegisterProp">A delegate that is used to setup the property metadata.</param>
        public void Property<TProp>(
            Expression<Func<T, TProp>> expressionProperty,
            Action<IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>> actionRegisterProp)
        {
            // getting the property name
            var propertyName = ExpressionHelper.GetPropertyName(expressionProperty);
            var index = this.ViewItems.Count;

            // getting the property with that name
            PropertyMetadataBuilder propMeta;
            if (!this.properties.TryGetValue(propertyName, out propMeta))
            {
                this.properties[propertyName] = propMeta = new PropertyMetadataBuilder<TProp, IUIContext<TProp, TParentUIContext>>(index, propertyName);
                this.ViewItems.Add(propMeta);
            }

            var propMetaGeneric = (PropertyMetadataBuilder<TProp, IUIContext<TProp, TParentUIContext>>)propMeta;
            actionRegisterProp(
                new PropertyMetadataFluentBuilder<TProp, IUIContext<TProp, TParentUIContext>>(
                    propMetaGeneric));
        }

        /// <summary>
        /// Includes a group template to use as default values source.
        /// </summary>
        /// <param name="groupTypeName">Name of the group template to use.</param>
        public void Template(string groupTypeName)
        {
            this.Templates.Add(groupTypeName);
        }

        /// <summary>
        /// Includes a group template to use as default values source.
        /// </summary>
        /// <param name="template">A group template used as a pre-configuration of the group being created.</param>
        public void Template(INamedGroupOrTemplate template)
        {
            var groupType2 = this.Context.DeclareTemplate(template.Name);
            if (template != groupType2)
                throw new ArgumentException("The passed group type is not in the current metadata context.");

            this.Templates.Add(template.Name);
        }

        /// <summary>
        /// Includes a group of properties in the view.
        /// </summary>
        /// <param name="groupName">Name of the group that is being included in the view.</param>
        /// <param name="actionRegisterGroup">A delegate that is used to setup the group metadata.</param>
        public void Group(
            string groupName,
            Action<SubGroupMetadataFluentBuilder<T, TParentUIContext>> actionRegisterGroup)
        {
            var index = this.ViewItems.Count;
            SubGroupMetadataBuilder<T, TParentUIContext> group;
            if (!this.groups.TryGetValue(groupName, out group))
            {
                this.groups[groupName] = group = new SubGroupMetadataBuilder<T, TParentUIContext>(this.Context, index, groupName);
                this.ViewItems.Add(group);
            }

            actionRegisterGroup(new SubGroupMetadataFluentBuilder<T, TParentUIContext>(group));
        }
    }
}