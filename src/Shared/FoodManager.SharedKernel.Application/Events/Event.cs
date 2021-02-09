using FoodManager.SharedKernel.Application.Messages;
using MediatR;

namespace FoodManager.SharedKernel.Application.Events
{
    public abstract class Event<TEvent>: Message<TEvent>, INotification where TEvent : Event<TEvent>
    {
        
    }
}