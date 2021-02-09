using System.Threading;
using System.Threading.Tasks;
using FoodManager.SharedKernel.Application.CommandResults;
using FoodManager.SharedKernel.Application.Commands;
using FoodManager.SharedKernel.Application.Events;
using FoodManager.SharedKernel.Application.Interfaces;
using MediatR;

namespace FoodManager.SharedKernel.Application.Handlers
{
    public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand, CommandResult> where TCommand : Command<TCommand>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CommandHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }
        
        protected async Task<CommandResult> SendCommandAsync<TNewCommand>(TNewCommand command)
            where TNewCommand : Command<TNewCommand> => await _mediatorHandler.SendAsync(command);

        protected async Task PublishEventAsync<TEvent>(TEvent @event) where TEvent : Event<TEvent> =>
            await _mediatorHandler.PublishAsync(@event);

        protected CommandResult CreateCommandResult(bool success, string message = "", object data = null)
            => new(success, message, data);

        public abstract Task<CommandResult> Handle(TCommand request, CancellationToken cancellationToken);

    }
}