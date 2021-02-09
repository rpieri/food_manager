using System;

namespace FoodManager.SharedKernel.Application.CommandResults
{
    public class CommandResult
    {
        public CommandResult(bool success, string message = "", object data = null)
        {
            Message = message;
            Success = success;
            Data = data;
        }

        public String Message { get; private set; }
        public bool Success { get; private set; }
        public object Data { get; protected set; }
        public bool HasAProblem => !Success;
    }
}