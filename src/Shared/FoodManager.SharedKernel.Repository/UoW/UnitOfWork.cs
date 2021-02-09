using System.Threading.Tasks;
using FoodManager.SharedKernel.Application.Interfaces;
using FoodManager.SharedKernel.Repository.Contexts;

namespace FoodManager.SharedKernel.Repository.UoW
{
    public sealed class UnitOfWork<TContext> : IUnitOfWork where TContext : Context<TContext>
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context) => _context = context;

        public ValueTask DisposeAsync() => _context.DisposeAsync();

        public async Task<bool> CommitAsync() => await _context.SaveChangesAsync() > 0;
    }
}