using System.Threading.Tasks;
using FoodManager.SharedKernel.Application.CommandResults;
using FoodManager.SharedKernel.Application.Commands;
using FoodManager.SharedKernel.Application.Interfaces;

namespace FoodManager.SharedKernel.Application.Handlers
{
    public abstract class QueryCommandHandler<TQueryCommand>: CommandHandler<TQueryCommand> where TQueryCommand : QueryCommand<TQueryCommand>
    {
        protected QueryCommandHandler(IMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
        }
        
        protected Task<QueryCommandResult> CreateQueryCommandResult(bool success, object data, int count) 
            => Task.FromResult(new QueryCommandResult(success, data: data, count: count));
    }
}