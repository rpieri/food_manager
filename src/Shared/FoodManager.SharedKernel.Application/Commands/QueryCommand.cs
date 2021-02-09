namespace FoodManager.SharedKernel.Application.Commands
{
    public abstract class QueryCommand<TQueryCommand>: Command<TQueryCommand> where TQueryCommand : Command<TQueryCommand>
    {
        
    }
}