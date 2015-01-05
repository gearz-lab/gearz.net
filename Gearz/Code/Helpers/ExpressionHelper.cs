using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace Gearz.Code.Helpers
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
            return System.Web.Mvc.ExpressionHelper.GetExpressionText(propertyExpression);
        }

        public static PropertyInfo GetPropertyFromMemberExpression<TEntity>(Expression<Func<TEntity, Object>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            var body = expression.Body as MemberExpression;

            if (body == null)
                throw new ArgumentException("'expression' should be a member expression");

            return (PropertyInfo)body.Member;
        }
    }
}