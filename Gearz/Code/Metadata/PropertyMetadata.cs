using System;
using System.Linq.Expressions;
using Gearz.Code.Helpers;

namespace Gearz.Code.Metadata
{
    public class PropertyMetadata
    {
        public PropertyMetadata(string name, string displayName = null)
        {
            if (name == null) throw new ArgumentNullException("name");
            this.DisplayName = displayName ?? this.Name;
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool Required { get; set; }

        public bool Invisible { get; set; }

        public bool Invalid { get; set; }
    }

    public class PropertyMetadata<TEntity> : PropertyMetadata
    {
        public PropertyMetadata(string name, string displayName = null) : base(name, displayName)
        {

        }

        public PropertyMetadata(Expression<Func<TEntity, Object>> propertyExpression) : base(ExpressionHelper.GetPropertyNameFromMemberExpression(propertyExpression))
        {
            
        }
    }
}