using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Gearz.Core.Helpers;
using Newtonsoft.Json;

namespace Gearz.Core.Metadata
{
    /// <summary>
    /// Contains all metadata for an entity
    /// </summary>
    public class EntityMetadata : IEquatable<EntityMetadata>
    {
        public EntityMetadata(Type entityType, string name = null, string displayName = null)
        {
            if (entityType == null) throw new ArgumentNullException("entityType");

            this.EntityType = entityType;
            this.Properties = new Dictionary<string, PropertyMetadata>();
            this.Name = name ?? this.EntityType.Name;
            this.DisplayName = displayName ?? this.Name;
        }

        /// <summary>
        /// Entity type of which this class contains metadata
        /// </summary>
        [JsonIgnore]
        public Type EntityType { get; private set; }

        /// <summary>
        /// The entity name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The entity display name
        /// </summary>
        public string DisplayName { get; set; }


        /// <summary>
        /// Collection of properties for this entity
        /// </summary>
        public Dictionary<string, PropertyMetadata> Properties { get; private set; }

        public bool Equals(EntityMetadata other)
        {
            return this.EntityType == other.EntityType;
        }

        public override int GetHashCode()
        {
            return this.EntityType.GetHashCode();
        }
    }

    /// <summary>
    /// Contains all metadata for an entity. Generic variant of the type 'EntityMetadata'
    /// </summary>
    public class EntityMetadata<TEntity> : EntityMetadata
    {
        public EntityMetadata(string name = null, string displayName = null)
            : base(typeof(TEntity), name, displayName)
        {

        }

        public PropertyMetadata Property(Expression<Func<TEntity, Object>> propertyExpression)
        {
            if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");

            var propertyName = ExpressionHelper.GetPropertyNameFromMemberExpression(propertyExpression);
            if (!this.Properties.ContainsKey(propertyName))
                this.Properties.Add(propertyName, new PropertyMetadata<TEntity>(propertyExpression));
            return (PropertyMetadata<TEntity>)this.Properties[propertyName];
        }
    }
}
