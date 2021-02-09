using FoodManager.SharedKernel.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodManager.SharedKernel.Repository.Mappings
{
    public abstract class Mapping<TEntity>: IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        private readonly string _tableName;

        public Mapping(string tableName) => _tableName = tableName;

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(_tableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Removed);
            builder.Property(x => x.RemovedDate);
            builder.Property(x => x.LastUpdateDate);
            builder.Property(x => x.CreatedDate);
            builder.Property(x => x.TenantId);

            EntityMapping(builder);
            
            builder.HasQueryFilter(x => !x.Removed);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.Valid);
            builder.Ignore(x => x.Invalid);
            
        }

        protected abstract void EntityMapping(EntityTypeBuilder<TEntity> builder);
    }
}