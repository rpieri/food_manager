using Microsoft.EntityFrameworkCore;

namespace FoodManager.SharedKernel.Repository.Contexts
{
    public abstract class Context<TContext> : DbContext where TContext : Context<TContext>
    {
        public Context(DbContextOptions<TContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public void ExecuteMigrations() => Database.Migrate();
    }
}