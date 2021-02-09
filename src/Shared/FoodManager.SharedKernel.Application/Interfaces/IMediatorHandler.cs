using System.Threading.Tasks;
using FoodManager.SharedKernel.Application.CommandResults;
using FoodManager.SharedKernel.Application.Events;
using MediatR;

namespace FoodManager.SharedKernel.Application.Interfaces
{
    public interface IMediatorHandler
    {
        Task<CommandResult> SendAsync<TCommand>(TCommand command) where TCommand : IRequest<CommandResult>;
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event<TEvent>;
    }
}