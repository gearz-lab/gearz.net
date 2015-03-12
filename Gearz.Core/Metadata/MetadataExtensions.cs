using System;
using System.Linq.Expressions;
using Gearz.Core.Metadata.Builders;
using Lambda2Js;

namespace Gearz.Core.Metadata
{
    public static class MetadataExtensions
    {
        #region InvisibleWhen

        public static TSelf InvisibleWhen<TSelf>(
            this IGroupItemMetadataFluentBuilder<TSelf> groupItemMetadataFluentBuilder,
            string expr)
            where TSelf : IGroupItemMetadataFluentBuilder<TSelf>
        {
            groupItemMetadataFluentBuilder.Hint("InvisibleWhen", expr);
            return (TSelf)groupItemMetadataFluentBuilder;
        }

        public static SubGroupMetadataFluentBuilder<TProp, IUIContext<T, TParentUIContext>> InvisibleWhen<TProp, T, TParentUIContext>(
            this SubGroupMetadataFluentBuilder<TProp, IUIContext<T, TParentUIContext>> groupItemMetadataFluentBuilder,
            Expression<Func<T, TParentUIContext, bool>> expr)
            where TParentUIContext : IUIContext
        {
            var exprStr = expr.CompileToJavascript();
            groupItemMetadataFluentBuilder.Hint("InvisibleWhen", exprStr);
            return groupItemMetadataFluentBuilder;
        }

        public static SubGroupMetadataFluentBuilder<TProp, UIContext<T, TParentUIContext>> InvisibleWhen<TProp, T, TParentUIContext>(
            this SubGroupMetadataFluentBuilder<TProp, UIContext<T, TParentUIContext>> groupItemMetadataFluentBuilder,
            Expression<Func<T, bool>> expr)
            where TParentUIContext : IUIContext
        {
            var exprStr = expr.CompileToJavascript();
            groupItemMetadataFluentBuilder.Hint("InvisibleWhen", exprStr);
            return groupItemMetadataFluentBuilder;
        }

        public static IPropertyMetadataFluentBuilder<TProp, UIContext<TProp, UIContext<T, TParentUIContext>>>
            InvisibleWhen<TProp, T, TParentUIContext>(
            this IPropertyMetadataFluentBuilder<TProp, UIContext<TProp, UIContext<T, TParentUIContext>>> groupItemMetadataFluentBuilder,
            Expression<Func<T, UIContext<T, TParentUIContext>, bool>> expr)
            where TParentUIContext : UIContext
        {
            var exprStr = expr.CompileToJavascript();
            groupItemMetadataFluentBuilder.Hint("InvisibleWhen", exprStr);
            return groupItemMetadataFluentBuilder;
        }

        public static IPropertyMetadataFluentBuilder<TProp, UIContext<TProp, UIContext<T, TParentUIContext>>>
            InvisibleWhen<TProp, T, TParentUIContext>(
            this IPropertyMetadataFluentBuilder<TProp, UIContext<TProp, UIContext<T, TParentUIContext>>> groupItemMetadataFluentBuilder,
            Expression<Func<T, bool>> expr)
            where TParentUIContext : UIContext
        {
            var exprStr = expr.CompileToJavascript();
            groupItemMetadataFluentBuilder.Hint("InvisibleWhen", exprStr);
            return groupItemMetadataFluentBuilder;
        }

        #endregion

        #region InvalidWhen

        /// <summary>
        /// Defines when the group or property is considered invalid, by specifying a text expression.
        /// Available variables:
        ///  1. All properties in the scope
        ///  2. The 'value' of the property (not valid for groups)
        /// </summary>
        /// <typeparam name="T">Type of the scope object.</typeparam>
        /// <typeparam name="TParentUIContext">Type of the parent UI context.</typeparam>
        /// <typeparam name="TSelf">Type of the return value.</typeparam>
        /// <param name="groupItemMetadataFluentBuilder">The group item to make invalid upon a condition.</param>
        /// <param name="expr">The text expression representing the condition.</param>
        /// <returns>The original object that allows a fluent code style.</returns>
        public static TSelf InvalidWhen<T, TParentUIContext, TSelf>(
            this IGroupItemMetadataFluentBuilderEx<T, TParentUIContext, TSelf> groupItemMetadataFluentBuilder,
            string expr)
            where TParentUIContext : IUIContext
            where TSelf : IMetadataFluentBuilderEx<T, TParentUIContext, TSelf>
        {
            groupItemMetadataFluentBuilder.Hint("InvalidWhen", expr);
            return (TSelf)groupItemMetadataFluentBuilder;
        }

        public static SubGroupMetadataFluentBuilder<TProp, UIContext<T, TParentUIContext>> InvalidWhen<TProp, T, TParentUIContext>(
            this SubGroupMetadataFluentBuilder<TProp, UIContext<T, TParentUIContext>> groupItemMetadataFluentBuilder,
            Expression<Func<T, TParentUIContext, bool>> expr)
            where TParentUIContext : IUIContext
        {
            var exprStr = expr.CompileToJavascript();
            groupItemMetadataFluentBuilder.Hint("InvalidWhen", exprStr);
            return groupItemMetadataFluentBuilder;
        }

        public static SubGroupMetadataFluentBuilder<TProp, UIContext<T, TParentUIContext>> InvalidWhen<TProp, T, TParentUIContext>(
            this SubGroupMetadataFluentBuilder<TProp, UIContext<T, TParentUIContext>> groupItemMetadataFluentBuilder,
            Expression<Func<T, bool>> expr)
            where TParentUIContext : IUIContext
        {
            var exprStr = expr.CompileToJavascript();
            groupItemMetadataFluentBuilder.Hint("InvalidWhen", exprStr);
            return groupItemMetadataFluentBuilder;
        }

        public static IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, IUIContext<T, TParentUIContext>>>
            InvalidWhen<TProp, T, TParentUIContext>(
            this IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, IUIContext<T, TParentUIContext>>> groupItemMetadataFluentBuilder,
            Expression<Func<TProp, T, IUIContext<T, TParentUIContext>, bool>> expr)
            where TParentUIContext : IUIContext
        {
            var exprStr = expr.CompileToJavascript();
            groupItemMetadataFluentBuilder.Hint("InvalidWhen", exprStr);
            return groupItemMetadataFluentBuilder;
        }

        public static IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, IUIContext<T, TParentUIContext>>>
            InvalidWhen<TProp, T, TParentUIContext>(
            this IPropertyMetadataFluentBuilder<TProp, IUIContext<TProp, IUIContext<T, TParentUIContext>>> groupItemMetadataFluentBuilder,
            Expression<Func<TProp, T, bool>> expr)
            where TParentUIContext : IUIContext
        {
            var exprStr = expr.CompileToJavascript();
            groupItemMetadataFluentBuilder.Hint("InvalidWhen", exprStr);
            return groupItemMetadataFluentBuilder;
        }

        #endregion
    }
}
