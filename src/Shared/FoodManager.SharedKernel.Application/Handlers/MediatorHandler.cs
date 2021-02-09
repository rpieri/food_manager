using System.Threading.Tasks;
using FoodManager.SharedKernel.Application.CommandResults;
using FoodManager.SharedKernel.Application.Events;
using FoodManager.SharedKernel.Application.Interfaces;
using MediatR;

namespace FoodManager.SharedKernel.Application.Handlers
{
    internal sealed class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommandResult> SendAsync<TCommand>(TCommand command)
            where TCommand : IRequest<CommandResult> => await _mediator.Send(command);

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event<TEvent>
        {
            await Task.Yield();
            await _mediator.Publish(@event);
        }
    }
}