using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Gearz.Code.Helpers
{
    /// <summary>
    /// Exposes methods to deal with text-expressions (https://github.com/masbicudo/gearz.net/issues/3)
    /// </summary>
    public static class TextExpressionHelper
    {
        /// <summary>
        /// Converts a boolean to a text-expression;
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string BoolToTextExpression(bool value)
        {
            return value.ToString().ToLower();
        }

        /// <summary>
        /// Converts a C# expression tree into a text-expression (https://github.com/masbicudo/gearz.net/issues/3)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string ExpressionToTextExpression<TEntity>(Expression<Func<TEntity, object>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            throw new NotImplementedException();
        }
    }
}