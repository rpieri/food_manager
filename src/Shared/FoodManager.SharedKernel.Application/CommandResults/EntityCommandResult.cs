using System.Collections.Generic;
using System.Linq;

namespace FoodManager.SharedKernel.Application.CommandResults
{
    public sealed class EntityCommandResult : CommandResult
    {
        public EntityCommandResult(bool success, string message = "", object data = null, IList<string> validationErrors = null) : base(success, message, data)
        {
            ValidationErrors = validationErrors ?? new List<string>();
        }
        
        public IList<string> ValidationErrors { get; private set; }
        public bool HasValidationError => ValidationErrors.Any();
        public void AddValidationError(string error) => ValidationErrors.Add(error);
    }
}