using System.Threading.Tasks;
using FoodManager.SharedKernel.Authentication.Services;
using FoodManager.SharedKernel.Domain.Models;
using FoodManager.SharedKernel.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FoodManager.SharedKernel.Repository.Repositories
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : Entity where TContext : Context<TContext>
    {
        private readonly TContext _context;
        private readonly IAuthenticationService _authenticationService;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(TContext context, IAuthenticationService authenticationService)
        {
            _dbSet = context.Set<TEntity>();
            _context = context;
            _authenticationService = authenticationService;
        }
        public async Task AddAsync(TEntity entity)
        {
            entity.SetCompanyCode(_authenticationService.GetTenantId());
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            entity.Update();
            await Task.Run(() => Update(entity));
        }

        public async Task RemoveAsync(TEntity entity)
        {
            entity.Remove();
            await Task.Run(() => Update(entity));
        }

        private void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
        }
    }
}