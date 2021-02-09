using System;

namespace FoodManager.SharedKernel.Application.Messages
{
    public abstract class Message<TMessage> where TMessage : Message<TMessage>
    {
        public Message()
        {
            Timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            MessageType = typeof(TMessage).Name;
        }
        public Int32 Timestamp { get; private set; }
        public string MessageType { get; private set; }
    }
}