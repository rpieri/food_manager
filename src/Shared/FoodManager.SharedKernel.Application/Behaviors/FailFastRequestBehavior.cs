using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodManager.SharedKernel.Application.CommandResults;
using FoodManager.SharedKernel.Application.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FoodManager.SharedKernel.Application.Behaviors
{
    public sealed class FailFastRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : Command<TRequest> where TResponse : CommandResult
    {
        private readonly ILogger<TRequest> _logger;

        public FailFastRequestBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is not EntityCommand<TRequest>) return next();
            var entityRequest = request as EntityCommand<TRequest>;
            if (entityRequest.Validate()) return next();
            var log = new StringBuilder();
            var response = new EntityCommandResult(false);
            entityRequest.ValidationResult.Errors.ToList().ForEach(error =>
            {
                response.AddValidationError(error.ErrorMessage);
                log.Append(error.ErrorMessage);
            });
            _logger.LogError(log.ToString());
            return Task.FromResult(response as TResponse);
        }
    }
}