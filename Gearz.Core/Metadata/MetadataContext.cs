using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Gearz.Core.Helpers;

namespace Gearz.Core.Metadata
{
    public class MetadataContext
    {
        private readonly Dictionary<Type, EntityViewMetadataBuilder> entities =
            new Dictionary<Type, EntityViewMetadataBuilder>();

        public MetadataContext()
        {
        }

        /// <summary>
        /// Indicates that metadata for the given type exists,
        /// returning the associated metadata object (either created or already existing).
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <returns>An <see cref="EntityViewMetadataBuilder"/> containing meta information about the type.</returns>
        public EntityViewMetadataBuilder<TEntity> EntityView<TEntity>()
        {
            EntityViewMetadataBuilder result;
            if (!this.entities.TryGetValue(typeof(TEntity), out result))
                this.entities[typeof(TEntity)] = result = new EntityViewMetadataBuilder<TEntity>();

            return (EntityViewMetadataBuilder<TEntity>)result;
        }

        public GroupTypeMetadataBuilder GroupType(string groupTypeName)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, EntityViewMetadataJsonModel> GetJsonModel()
        {
            return this.entities.ToDictionary(x => x.Key.Name, x => x.Value.GetJsonModel());
        }
    }

    public class EntityViewMetadataJsonModel
    {
    }

    public abstract class EntityViewMetadataBuilder
    {
        internal EntityViewMetadataBuilder()
        {
        }

        public abstract EntityViewMetadataJsonModel GetJsonModel();
    }

    public class EntityViewMetadataBuilder<T> : EntityViewMetadataBuilder,
        IGroupMetadataBuilder<T, EntityViewMetadataBuilder<T>>
    {
        private readonly Dictionary<string, PropertyMetadataBuilder> properties =
            new Dictionary<string, PropertyMetadataBuilder>();

        private readonly List<string> editorNames = new List<string>();
        private readonly List<Expression<Func<UIContext<T>, string>>> displayNames = new List<Expression<Func<UIContext<T>, string>>>();

        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// This is a final value, since it is a constant.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public EntityViewMetadataBuilder<T> Display(string text)
        {
            this.displayNames.Add(Expression.Lambda<Func<UIContext<T>, string>>(Expression.Constant(text)));
            return this;
        }

        /// <summary>
        /// Includes a text to display as a caption of this entity.
        /// When multiple are added, the first one that can build a valid string is used.
        /// A valid string is non-null nor empty string, without throwing errors.
        /// </summary>
        /// <param name="textBuilderExpression">Lambda expression that can build the text to display.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public EntityViewMetadataBuilder<T> Display(Expression<Func<UIContext<T>, string>> textBuilderExpression)
        {
            this.displayNames.Add(textBuilderExpression);
            return this;
        }

        /// <summary>
        /// Includes an editor name that can be used with this entity type.
        /// If more than one is added, then the UI builder will choose the alternative that best suits,
        /// based on the affinity of the component with the context where it is going to be inserted.
        /// </summary>
        /// <param name="editorName">The editor component name, used to edit this entity.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public EntityViewMetadataBuilder<T> Editor(string editorName)
        {
            var idx = this.editorNames.BinarySearch(editorName);
            if (idx < 0)
                this.editorNames.Insert(~idx, editorName);

            return this;
        }

        public EntityViewMetadataBuilder<T> Hint(string hintName, object value)
        {
            throw new NotImplementedException();
        }

        public EntityViewMetadataBuilder<T> Property<TProp>(Expression<Func<T, TProp>> actionRegisterProp)
        {
            throw new NotImplementedException();
        }

        public EntityViewMetadataBuilder<T> Property<TProp>(
            Expression<Func<T, TProp>> expressionProperty,
            Action<PropertyMetadataBuilder<T, TProp>> actionRegisterProp)
        {
            // getting the property name
            var propertyName = ExpressionHelper.GetPropertyName(expressionProperty);

            // getting the property with that name
            PropertyMetadataBuilder propMeta;
            if (!this.properties.TryGetValue(propertyName, out propMeta))
                this.properties[propertyName] = propMeta = new PropertyMetadataBuilder<T, TProp>();

            actionRegisterProp((PropertyMetadataBuilder<T, TProp>)propMeta);

            return this;
        }

        public EntityViewMetadataBuilder<T> Property<TProp>(
            string propertyName,
            Action<PropertyMetadataBuilder<T, TProp>> actionRegisterProp)
        {
            // getting the property with that name
            PropertyMetadataBuilder propMeta;
            if (!this.properties.TryGetValue(propertyName, out propMeta))
                this.properties[propertyName] = propMeta = new PropertyMetadataBuilder<T, TProp>();

            actionRegisterProp((PropertyMetadataBuilder<T, TProp>)propMeta);

            return this;
        }

        public EntityViewMetadataBuilder<T> Property<TProp>(
            string propertyName,
            out VirtualProperty<TProp> virtualProperty,
            Action<PropertyMetadataBuilder<T, TProp>> actionRegisterProp)
        {
            // getting the property with that name
            PropertyMetadataBuilder propMeta;
            if (!this.properties.TryGetValue(propertyName, out propMeta))
                this.properties[propertyName] = propMeta = new PropertyMetadataBuilder<T, TProp>();

            actionRegisterProp((PropertyMetadataBuilder<T, TProp>)propMeta);

            virtualProperty = new VirtualProperty<TProp>(propertyName);

            return this;
        }

        public EntityViewMetadataBuilder<T> Group(
            string groupTypeName,
            string groupName,
            Action<EntityViewMetadataBuilder<T>> func1)
        {
            throw new NotImplementedException();
        }

        public EntityViewMetadataBuilder<T> Group(
            GroupTypeMetadataBuilder groupType,
            string groupName,
            Action<EntityViewMetadataBuilder<T>> func1)
        {
            throw new NotImplementedException();
        }

        public override EntityViewMetadataJsonModel GetJsonModel()
        {
            throw new NotImplementedException();
        }
    }

    public class GroupMetadataBuilder<TParent> :
        IGroupMetadataBuilder<TParent, GroupMetadataBuilder<TParent>>
    {
        public GroupMetadataBuilder<TParent> Display(string text)
        {
            throw new NotImplementedException();
        }

        public GroupMetadataBuilder<TParent> Display(Expression<Func<UIContext<TParent>, string>> textBuilderExpression)
        {
            throw new NotImplementedException();
        }

        public GroupMetadataBuilder<TParent> Editor(string editorName)
        {
            throw new NotImplementedException();
        }

        public GroupMetadataBuilder<TParent> Hint(string hintName, object value)
        {
            throw new NotImplementedException();
        }

        public GroupMetadataBuilder<TParent> Property<TProp>(Expression<Func<TParent, TProp>> actionRegisterProp)
        {
            throw new NotImplementedException();
        }

        public GroupMetadataBuilder<TParent> Property<TProp>(
            Expression<Func<TParent, TProp>> expressionProperty,
            Action<PropertyMetadataBuilder<TParent, TProp>> actionRegisterProp)
        {
            throw new NotImplementedException();
        }

        public GroupMetadataBuilder<TParent> Property<TProp>(
            string propertyName,
            Action<PropertyMetadataBuilder<TParent, TProp>> actionRegisterProp)
        {
            throw new NotImplementedException();
        }

        public GroupMetadataBuilder<TParent> Property<TProp>(
            string propertyName,
            out VirtualProperty<TProp> virtualProperty,
            Action<PropertyMetadataBuilder<TParent, TProp>> actionRegisterProp)
        {
            throw new NotImplementedException();
        }

        public GroupMetadataBuilder<TParent> Group(
            string groupTypeName,
            string groupName,
            Action<GroupMetadataBuilder<TParent>> func1)
        {
            throw new NotImplementedException();
        }

        public GroupMetadataBuilder<TParent> Group(
            GroupTypeMetadataBuilder groupType,
            string groupName,
            Action<GroupMetadataBuilder<TParent>> func1)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class PropertyMetadataBuilder
    {
        internal PropertyMetadataBuilder()
        {
        }
    }

    public class PropertyMetadataBuilder<TParent, TProp> : PropertyMetadataBuilder
    {
        public PropertyMetadataBuilder<TParent, TProp> Editor(string editorName)
        {
            throw new NotImplementedException();
        }

        public PropertyMetadataBuilder<TParent, TProp> Display(string text)
        {
            throw new NotImplementedException();
        }
    }

    public class GroupTypeMetadataBuilder
    {
        public GroupTypeMetadataBuilder Display(string text)
        {
            throw new NotImplementedException();
        }

        public GroupTypeMetadataBuilder Editor(string editorName)
        {
            throw new NotImplementedException();
        }

        public GroupTypeMetadataBuilder Hint(string hintName, object value)
        {
            throw new NotImplementedException();
        }
    }

    public static class XptoExtensions
    {
        public static TSelf SomeOtherAttribute<T, TSelf>(
            this IGroupMetadataBuilder<T, TSelf> groupTypeMetadataBuilder,
            string str)
        {
            groupTypeMetadataBuilder.Hint("SomeOtherAttribute", str);
            return (TSelf)groupTypeMetadataBuilder;
        }

        public static PropertyMetadataBuilder<TParent, TProp> InvisibleWhen<TParent, TProp>(
            this PropertyMetadataBuilder<TParent, TProp> groupTypeMetadataBuilder,
            string expr)
        {
            throw new NotImplementedException();
        }

        public static PropertyMetadataBuilder<TParent, TProp> InvisibleWhen<TParent, TProp>(
            this PropertyMetadataBuilder<TParent, TProp> groupTypeMetadataBuilder,
            Expression<Func<TParent, bool>> expr)
        {
            throw new NotImplementedException();
        }
    }
}
