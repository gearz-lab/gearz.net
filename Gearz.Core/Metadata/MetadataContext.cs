using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Gearz.Core.Metadata
{
    public class MetadataContext
    {
        private readonly Dictionary<Type, GroupMetadataBuilder> entities =
            new Dictionary<Type, GroupMetadataBuilder>();

        public MetadataContext()
        {
        }

        /// <summary>
        /// Indicates that metadata for the given type exists,
        /// returning the associated metadata object (either created or already existing).
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <returns>An <see cref="GroupMetadataBuilder"/> containing meta information about the type.</returns>
        public EntityMetadataFluentBuilder<TEntity> EntityView<TEntity>()
        {
            GroupMetadataBuilder result;
            if (!this.entities.TryGetValue(typeof(TEntity), out result))
                this.entities[typeof(TEntity)] = result = new EntityMetadataBuilder<TEntity>(this);

            return new EntityMetadataFluentBuilder<TEntity>((EntityMetadataBuilder<TEntity>)result);
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

    public abstract class GroupMetadataBuilder
    {
        internal GroupMetadataBuilder()
        {
        }

        public abstract EntityViewMetadataJsonModel GetJsonModel();
    }

    public abstract class PropertyMetadataBuilder
    {
        internal PropertyMetadataBuilder()
        {
        }
    }

    public class GroupTypeMetadataBuilder
    {
        public GroupTypeMetadataBuilder(string templateName)
        {
            this.TemplateName = templateName;
        }

        public string TemplateName { get; private set; }

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
        public static TSelf SomeOtherAttribute<T, TParentUIContext, TSelf>(
            this GroupMetadataFluentBuilder<T, TParentUIContext, TSelf> groupMetadataFluentBuilder,
            string str)
            where TParentUIContext : UIContext
            where TSelf : class
        {
            groupMetadataFluentBuilder.Hint("SomeOtherAttribute", str);
            return groupMetadataFluentBuilder as TSelf;
        }

        public static TSelf InvisibleWhen<TProp, TParentUIContext, TSelf>(
            this IGroupItemMetadataFluentBuilder<TProp, TParentUIContext, TSelf> groupItemMetadataFluentBuilder,
            string expr)
            where TParentUIContext : UIContext
            where TSelf : class
        {
            groupItemMetadataFluentBuilder.Hint("InvisibleWhen", expr);
            return groupItemMetadataFluentBuilder as TSelf;
        }

        public static SubGroupMetadataFluentBuilder<TProp, UIContext<T, TParentUIContext>> InvisibleWhen<TProp, T, TParentUIContext>(
            this SubGroupMetadataFluentBuilder<TProp, UIContext<T, TParentUIContext>> groupItemMetadataFluentBuilder,
            Expression<Func<T, TParentUIContext, bool>> expr)
            where TParentUIContext : UIContext
        {
            throw new NotImplementedException();
            var exprStr = expr.ToString();
            groupItemMetadataFluentBuilder.Hint("InvisibleWhen", exprStr);
            return groupItemMetadataFluentBuilder;
        }

        public static SubGroupMetadataFluentBuilder<TProp, UIContext<T, TParentUIContext>> InvisibleWhen<TProp, T, TParentUIContext>(
            this SubGroupMetadataFluentBuilder<TProp, UIContext<T, TParentUIContext>> groupItemMetadataFluentBuilder,
            Expression<Func<T, bool>> expr)
            where TParentUIContext : UIContext
        {
            throw new NotImplementedException();
            var exprStr = expr.ToString();
            groupItemMetadataFluentBuilder.Hint("InvisibleWhen", exprStr);
            return groupItemMetadataFluentBuilder;
        }

        public static PropertyMetadataFluentBuilder<TProp, UIContext<TProp, UIContext<T, TParentUIContext>>> InvisibleWhen<TProp, T, TParentUIContext>(
            this PropertyMetadataFluentBuilder<TProp, UIContext<TProp, UIContext<T, TParentUIContext>>> groupItemMetadataFluentBuilder,
            Expression<Func<T, UIContext<T, TParentUIContext>, bool>> expr)
            where TParentUIContext : UIContext
        {
            throw new NotImplementedException();
            var exprStr = expr.ToString();
            groupItemMetadataFluentBuilder.Hint("InvisibleWhen", exprStr);
            return groupItemMetadataFluentBuilder;
        }

        public static PropertyMetadataFluentBuilder<TProp, UIContext<TProp, UIContext<T, TParentUIContext>>> InvisibleWhen<TProp, T, TParentUIContext>(
            this PropertyMetadataFluentBuilder<TProp, UIContext<TProp, UIContext<T, TParentUIContext>>> groupItemMetadataFluentBuilder,
            Expression<Func<T, bool>> expr)
            where TParentUIContext : UIContext
        {
            throw new NotImplementedException();
            var exprStr = expr.ToString();
            groupItemMetadataFluentBuilder.Hint("InvisibleWhen", exprStr);
            return groupItemMetadataFluentBuilder;
        }
    }
}
