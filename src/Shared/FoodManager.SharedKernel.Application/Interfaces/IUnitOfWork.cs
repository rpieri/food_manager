using System;
using System.Threading.Tasks;

namespace FoodManager.SharedKernel.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<bool> CommitAsync();
    }
}