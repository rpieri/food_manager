using System;
using System.Threading.Tasks;
using FoodManager.SharedKernel.Domain.Models;

namespace FoodManager.SharedKernel.Repository.Repositories
{
    public interface IRepository<in TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }
}