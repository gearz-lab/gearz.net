using System.Collections.Generic;
using System.Collections.Immutable;
using Gearz.Core.Metadata.Builders;

namespace Gearz.Core.Metadata
{
    public class MetadataContext
    {
        private readonly Dictionary<string, IEntityMetadataBuilder> entities =
            new Dictionary<string, IEntityMetadataBuilder>();

        private readonly Dictionary<string, IEntityMetadataBuilder> templates =
            new Dictionary<string, IEntityMetadataBuilder>();

        /// <summary>
        /// Indicates that metadata for the given type exists,
        /// returning the associated metadata object (either created or already existing).
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <returns>An <see cref="EntityMetadataFluentBuilder{TEntity}"/> that allows a fluent style configuration code.</returns>
        public EntityMetadataFluentBuilder<TEntity> EntityView<TEntity>()
        {
            IEntityMetadataBuilder result;
            var name = typeof(TEntity).Name;
            if (!this.entities.TryGetValue(name, out result))
                this.entities[name] = result = new EntityMetadataBuilder<TEntity>(this, name);

            return new EntityMetadataFluentBuilder<TEntity>((EntityMetadataBuilder<TEntity>)result);
        }

        public EntityMetadataFluentBuilder<TEntity> EntityView<TEntity>(string name)
        {
            IEntityMetadataBuilder result;
            if (!this.entities.TryGetValue(name, out result))
                this.entities[name] = result = new EntityMetadataBuilder<TEntity>(this, name);

            return new EntityMetadataFluentBuilder<TEntity>((EntityMetadataBuilder<TEntity>)result);
        }

        public EntityMetadataFluentBuilder<dynamic> EntityView(string name)
        {
            IEntityMetadataBuilder result;
            if (!this.entities.TryGetValue(name, out result))
                this.entities[name] = result = new EntityMetadataBuilder<dynamic>(this, name);

            return new EntityMetadataFluentBuilder<dynamic>((EntityMetadataBuilder<dynamic>)result);
        }

        public TemplateEntityMetadataFluentBuilder<dynamic> DeclareTemplate(string groupTypeName)
        {
            IEntityMetadataBuilder result;
            if (!this.templates.TryGetValue(groupTypeName, out result))
                this.templates[groupTypeName] = result = new TemplateEntityMetadataBuilder<dynamic>(this, groupTypeName);

            return new TemplateEntityMetadataFluentBuilder<dynamic>((TemplateEntityMetadataBuilder<dynamic>)result);
        }

        public TemplateEntityMetadataFluentBuilder<T> DeclareTemplate<T>(string groupTypeName)
        {
            IEntityMetadataBuilder result;
            if (!this.templates.TryGetValue(groupTypeName, out result))
                this.templates[groupTypeName] = result = new TemplateEntityMetadataBuilder<T>(this, groupTypeName);

            return new TemplateEntityMetadataFluentBuilder<T>((TemplateEntityMetadataBuilder<T>)result);
        }

        public MetadataJsonModel GetJsonModel()
        {
            var entitiesJson = this.entities.ToImmutableDictionary(
                x => x.Key,
                x => x.Value.GetJsonModel());

            var templatesJson = this.templates.ToImmutableDictionary(
                x => x.Key,
                x => x.Value.GetJsonModel());

            var result = new MetadataJsonModel(entitiesJson, templatesJson);
            return result;
        }
    }
}
