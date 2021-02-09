using FoodManager.SharedKernel.Application.CommandResults;
using FoodManager.SharedKernel.Application.Messages;
using MediatR;

namespace FoodManager.SharedKernel.Application.Commands
{
    public abstract class Command<TCommand> : Message<TCommand>, IRequest<CommandResult> where TCommand : Command<TCommand>
    {
        
    }
}