using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Gearz.Core.Helpers;
using JetBrains.Annotations;

namespace Gearz.Core.Metadata
{
    public class SubGroupMetadataBuilder<T, TParentUIContext> : GroupMetadataBuilder<T, TParentUIContext>,
        IGroupMetadataBuilder<T, TParentUIContext>,
        IGroupItemMetadataBuilder<T, TParentUIContext>
        where TParentUIContext : UIContext
    {
        public SubGroupMetadataBuilder(MetadataContext context, int index, string groupName)
            : base(context, index, groupName)
        {
        }
    }

    public class EntityMetadataBuilder<T> : GroupMetadataBuilder<T, UIContext<T, RootUIContext>>
    {
        public EntityMetadataBuilder([NotNull] MetadataContext context)
            : base(context)
        {
        }
    }

    public abstract class GroupMetadataBuilder<T, TParentUIContext> : GroupMetadataBuilder,
        IGroupMetadataBuilder<T, TParentUIContext>
        where TParentUIContext : UIContext
    {
        private readonly List<object> viewItems = new List<object>();

        private readonly Dictionary<string, PropertyMetadataBuilder> properties
            = new Dictionary<string, PropertyMetadataBuilder>();

        private readonly Dictionary<string, SubGroupMetadataBuilder<T, TParentUIContext>> groups
            = new Dictionary<string, SubGroupMetadataBuilder<T, TParentUIContext>>();

        private readonly List<string> templates = new List<string>();

        private readonly List<string> editorNames = new List<string>();
        private readonly Dictionary<string, List<object>> hints = new Dictionary<string, List<object>>();

        private readonly List<Expression<Func<UIContext<T, TParentUIContext>, string>>> displayNames
            = new List<Expression<Func<UIContext<T, TParentUIContext>, string>>>();

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
        public string GroupName { get; private set; }

        /// <summary>
        /// Gets the metadata context in which metadata is being created.
        /// </summary>
        [NotNull]
        [UsedImplicitly]
        public MetadataContext Context { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupMetadataBuilder{T}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public GroupMetadataBuilder([NotNull] MetadataContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.Context = context;
        }

        protected GroupMetadataBuilder([NotNull] MetadataContext context, int index, [NotNull] string groupName)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (groupName == null)
                throw new ArgumentNullException("groupName");

            this.Context = context;
            this.Index = index;
            this.GroupName = groupName;
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
            this.displayNames.Add(Expression.Lambda<Func<UIContext<T, TParentUIContext>, string>>(Expression.Constant(text)));
        }

        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// When multiple are added, the first one that can build a valid string is used.
        /// A valid string is non-null nor empty string, without throwing errors.
        /// </summary>
        /// <param name="textBuilderExpression">Lambda expression that can build the text to display.</param>
        public void Display(Expression<Func<UIContext<T, TParentUIContext>, string>> textBuilderExpression)
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

        /// <summary>
        /// Indicates that a property participates in the view directly.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="expressionProperty">A lambda expression tree pointing to the property that appears in the view.</param>
        public void Property<TProp>(Expression<Func<T, TProp>> expressionProperty)
        {
            // getting the property name
            var propertyName = ExpressionHelper.GetPropertyName(expressionProperty);
            var index = this.viewItems.Count;

            // getting the property with that name
            PropertyMetadataBuilder propMeta;
            if (!this.properties.TryGetValue(propertyName, out propMeta))
            {
                this.properties[propertyName] = propMeta = new PropertyMetadataBuilder<TProp, TParentUIContext>(index, propertyName);
                this.viewItems.Add(propMeta);
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
            Action<PropertyMetadataFluentBuilder<TProp, UIContext<TProp, TParentUIContext>>> actionRegisterProp)
        {
            // getting the property name
            var propertyName = ExpressionHelper.GetPropertyName(expressionProperty);
            var index = this.viewItems.Count;

            // getting the property with that name
            PropertyMetadataBuilder propMeta;
            if (!this.properties.TryGetValue(propertyName, out propMeta))
            {
                this.properties[propertyName] = propMeta = new PropertyMetadataBuilder<TProp, UIContext<TProp, TParentUIContext>>(index, propertyName);
                this.viewItems.Add(propMeta);
            }

            actionRegisterProp(
                new PropertyMetadataFluentBuilder<TProp, UIContext<TProp, TParentUIContext>>(
                    (PropertyMetadataBuilder<TProp, UIContext<TProp, TParentUIContext>>)propMeta));
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
            Action<PropertyMetadataFluentBuilder<TProp, UIContext<TProp, TParentUIContext>>> actionRegisterProp)
        {
            var index = this.viewItems.Count;

            // getting the property with that name
            PropertyMetadataBuilder propMeta;
            if (!this.properties.TryGetValue(propertyName, out propMeta))
            {
                this.properties[propertyName] = propMeta = new PropertyMetadataBuilder<TProp, UIContext<TProp, TParentUIContext>>(index, propertyName);
                this.viewItems.Add(propMeta);
            }

            actionRegisterProp(
                new PropertyMetadataFluentBuilder<TProp, UIContext<TProp, TParentUIContext>>(
                    (PropertyMetadataBuilder<TProp, UIContext<TProp, TParentUIContext>>)propMeta));
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
            Action<PropertyMetadataFluentBuilder<TProp, UIContext<TProp, TParentUIContext>>> actionRegisterProp)
        {
            var index = this.viewItems.Count;

            // getting the property with that name
            PropertyMetadataBuilder propMeta;
            if (!this.properties.TryGetValue(propertyName, out propMeta))
            {
                this.properties[propertyName] = propMeta = new PropertyMetadataBuilder<TProp, UIContext<TProp, TParentUIContext>>(index, propertyName);
                this.viewItems.Add(propMeta);
            }

            actionRegisterProp(
                new PropertyMetadataFluentBuilder<TProp, UIContext<TProp, TParentUIContext>>(
                    (PropertyMetadataBuilder<TProp, UIContext<TProp, TParentUIContext>>)propMeta));

            virtualProperty = new VirtualProperty<TProp>(propertyName);
        }

        /// <summary>
        /// Includes a group template to use as default values source.
        /// </summary>
        /// <param name="groupTypeName">Name of the group template to use.</param>
        public void Template(string groupTypeName)
        {
            this.templates.Add(groupTypeName);
        }

        /// <summary>
        /// Includes a group template to use as default values source.
        /// </summary>
        /// <param name="groupType">A group template used as a pre-configuration of the group being created.</param>
        public void Template(GroupTypeMetadataBuilder groupType)
        {
            var groupType2 = this.Context.GroupType(groupType.TemplateName);
            if (groupType != groupType2)
                throw new ArgumentException("The passed group type is not in the current metadata context.");

            this.templates.Add(groupType.TemplateName);
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
            var index = this.viewItems.Count;
            SubGroupMetadataBuilder<T, TParentUIContext> group;
            if (!this.groups.TryGetValue(groupName, out group))
            {
                this.groups[groupName] = group = new SubGroupMetadataBuilder<T, TParentUIContext>(this.Context, index, groupName);
                this.viewItems.Add(group);
            }

            actionRegisterGroup(new SubGroupMetadataFluentBuilder<T, TParentUIContext>(group));
        }

        public override EntityViewMetadataJsonModel GetJsonModel()
        {
            return new EntityViewMetadataJsonModel(
                this.properties,
                this.groups,
                this.displayNames,
                this.editorNames,
                this.hints);
        }
    }
}