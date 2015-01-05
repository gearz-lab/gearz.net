using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    }
}