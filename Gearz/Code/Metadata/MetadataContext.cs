using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gearz.Code.Metadata
{
    public class MetadataContext
    {
        public MetadataContext()
        {
            this.Entities = new Dictionary<Type, EntityMetadata>();
        }

        public EntityMetadata<TEntity> Entity<TEntity>()
        {
            if (!this.Entities.ContainsKey(typeof(TEntity)))
                this.Entities.Add(typeof(TEntity), new EntityMetadata<TEntity>());
            return (EntityMetadata<TEntity>)this.Entities[typeof(TEntity)];
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<Type, EntityMetadata> Entities { get; private set; }
    }
}
