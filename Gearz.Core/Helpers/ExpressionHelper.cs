using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Gearz.Core.Helpers
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// Returns the property name based on the given member expression
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public static string GetPropertyNameFromMemberExpression<TEntity>(Expression<Func<TEntity, Object>> propertyExpression)
        {
            // the reason we're only returning 'ExpressionHelper.GetExpressionText' is because this
            // behavior might change over time
            //return System.Web.Mvc.ExpressionHelper.GetExpressionText(propertyExpression);
            throw new NotImplementedException("System.Web.Mvc.ExpressionHelper.GetExpressionText(propertyExpression)");
        }

        public static PropertyInfo GetPropertyFromMemberExpression<TEntity>(Expression<Func<TEntity, Object>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            var body = expression.Body as MemberExpression;

            if (body == null)
                throw new ArgumentException("'expression' should be a member expression");

            return (PropertyInfo)body.Member;
        }

        public static string GetPropertyName<T, TProp>(Expression<Func<T, TProp>> expressionProperty)
        {
            var names = new List<string>();

            var current = expressionProperty.Body;
            var root = expressionProperty.Parameters[0];

            while (current != null && current.NodeType != ExpressionType.Parameter)
            {
                if (current.NodeType == ExpressionType.Convert || current.NodeType == ExpressionType.ConvertChecked)
                    current = ((UnaryExpression)current).Operand;

                var memberExpression = current as MemberExpression;
                if (memberExpression == null)
                    throw new ArgumentException(
                        "The argument `expressionProperty` must contain only properties in the expression.",
                        "expressionProperty");

                names.Add(memberExpression.Member.Name);

                current = memberExpression.Expression;
            }

            if (!root.Equals(current))
                throw new ArgumentException(
                    "The argument `expressionProperty` must be a property expression, with the first parameter at the root.",
                    "expressionProperty");

            names.Reverse();
            var propName = string.Join(".", names);
            return propName;
        }
    }
}