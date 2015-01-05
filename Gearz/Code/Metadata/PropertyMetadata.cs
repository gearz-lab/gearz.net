using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Gearz.Code.Helpers;

namespace Gearz.Code.Metadata
{
    /// <summary>
    /// Metadata about a property
    /// </summary>
    public class PropertyMetadata
    {
        public PropertyMetadata(PropertyType type, string name, string displayName = null)
        {
            if (name == null) throw new ArgumentNullException("name");
            this.InvalidConditions = new HashSet<InvalidCondition>();
            this.Name = name;
            this.DisplayName = displayName ?? this.Name;
            this.Type = type;
        }

        /// <summary>
        /// The property type
        /// </summary>
        public PropertyType Type { get; set; }

        /// <summary>
        /// The property name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The property display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Whether or not the property is required, represented as a text-expression (https://github.com/masbicudo/gearz.net/issues/3)
        /// </summary>
        public string Required { get; protected set; }

        /// <summary>
        /// Whether or not the property is required, represented as a text-expression (https://github.com/masbicudo/gearz.net/issues/3)
        /// </summary>
        public PropertyMetadata IsRequired(bool value)
        {
            this.Required = TextExpressionHelper.BoolToTextExpression(value);
            return this;
        }

        /// <summary>
        /// Whether or not the property is invisible, represented as a text-expression (https://github.com/masbicudo/gearz.net/issues/3)
        /// </summary> 
        public string Invisible { get; protected set; }

        /// <summary>
        /// Whether or not the property is invisible, represented as a text-expression (https://github.com/masbicudo/gearz.net/issues/3)
        /// </summary>
        public PropertyMetadata IsInvisible(bool value)
        {
            this.Invisible = TextExpressionHelper.BoolToTextExpression(value);
            return this;
        }

        /// <summary>
        /// Whether or not the property is disabled, represented as a text-expression (https://github.com/masbicudo/gearz.net/issues/3)
        /// </summary> 
        public string Disabled { get; protected set; }

        /// <summary>
        /// Whether or not the property is disabled, represented as a text-expression (https://github.com/masbicudo/gearz.net/issues/3)
        /// </summary>
        public PropertyMetadata IsDisabled(bool value)
        {
            this.Disabled = TextExpressionHelper.BoolToTextExpression(value);
            return this;
        }

        /// <summary>
        /// Whether or not the property is invalid, represented as a text-expression
        /// ToDo: Determine how to treat multiple situations in which a property can be invalid. Each invalid status must have a type. This property is probably going to be an array
        /// </summary>
        public HashSet<InvalidCondition> InvalidConditions { get; set; }

        /// <summary>
        /// Represents a situation in which a property can be invalid. A property can be invalid in a variaty
        /// of ways and each of them have it's own error message
        /// </summary>
        public class InvalidCondition : IEquatable<InvalidCondition>
        {
            /// <summary>
            /// A text-expression that, if true, makes the property invalid (https://github.com/masbicudo/gearz.net/issues/3)
            /// </summary>
            public string Expression { get; set; }

            /// <summary>
            /// Error message for this given invalid condition
            /// </summary>
            public string ErrorMessage { get; set; }

            public bool Equals(InvalidCondition other)
            {
                return this.Expression == other.Expression;
            }

            public override int GetHashCode()
            {
                return this.Expression.GetHashCode();
            }
        }
    }

    /// <summary>
    /// Metadata about a property
    /// </summary>
    public class PropertyMetadata<TEntity> : PropertyMetadata
    {
        public PropertyMetadata(PropertyType type, string name, string displayName = null) : base(type, name, displayName)
        {

        }

        public PropertyMetadata(Expression<Func<TEntity, Object>> propertyExpression) : base(
            MetadataHelper.GetPropertyTypeFromType(ExpressionHelper.GetPropertyFromMemberExpression(propertyExpression).PropertyType),
            ExpressionHelper.GetPropertyNameFromMemberExpression(propertyExpression))
        {
            
        }

        public PropertyMetadata<TEntity> IsRequired(Expression<Func<TEntity, object>> value)
        {
            this.Required = TextExpressionHelper.ExpressionToTextExpression(value);
            return this;
        }

        public PropertyMetadata<TEntity> IsDisabled(Expression<Func<TEntity, object>> value)
        {
            this.Disabled = TextExpressionHelper.ExpressionToTextExpression(value);
            return this;
        }

        public PropertyMetadata<TEntity> IsInvisible(Expression<Func<TEntity, object>> value)
        {
            this.Invisible = TextExpressionHelper.ExpressionToTextExpression(value);
            return this;
        }
    }
}