namespace FoodManager.SharedKernel.Application.CommandResults
{
    public sealed class QueryCommandResult : CommandResult
    {
        public QueryCommandResult(bool success, string message = "", object data = null, int count = 0) : base(success, message, data)
        {
            Count = count;
        }

        public int Count { get; private set; }
    }
}