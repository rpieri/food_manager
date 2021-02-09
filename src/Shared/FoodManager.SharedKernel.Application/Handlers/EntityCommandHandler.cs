using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodManager.SharedKernel.Application.CommandResults;
using FoodManager.SharedKernel.Application.Commands;
using FoodManager.SharedKernel.Application.Interfaces;
using FoodManager.SharedKernel.Application.Mappings;
using FoodManager.SharedKernel.Domain.Models;
using FoodManager.SharedKernel.Domain.Notifications;
using Microsoft.Extensions.Logging;

namespace FoodManager.SharedKernel.Application.Handlers
{
    public abstract class EntityCommandHandler<TEntity, TCommand> : CommandHandler<TCommand>
        where TEntity : Entity where TCommand : EntityCommand<TCommand>
    {
        private readonly ILogger<TCommand> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notificationContext;

        public EntityCommandHandler(ILogger<TCommand> logger, IUnitOfWork unitOfWork,
            NotificationContext notificationContext, IMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
        }
        
                protected bool Valid(TEntity entity)
        {
            if (entity.Valid) return true;
            _notificationContext.AddNotification(entity.ValidationResult);
            return false;
        }

        protected async Task<EntityCommandResult> CommitAsync(TCommand command)
        {
            var entityMapper = await MapperCommandToEntityAsync(command);
            if (entityMapper.HasAProblem) return CreateEntityCommandResult(false);

            var entity = entityMapper.Entity;
            if (!await ValidateAndExecuteCommandAsync(entity)) return CreateEntityCommandResult(false);

            if (await _unitOfWork.CommitAsync())
                return CreateEntityCommandResult(true, entityMapper.Message, ReturnData(entity));

            return CreateEntityCommandResult(false);
        }
        
        protected async Task<bool> ValidateAndExecuteCommandAsync(TEntity entity)
        {
            if (Valid(entity))
            {
                await ExecuteCommandAsync(entity);
                return true;
            }
            return false;
        }

        private EntityCommandResult CreateEntityCommandResult(bool success, string message = "", object data = null)
        {
            var entityCommandResult = CreateCommandResult(success, message, data) as EntityCommandResult;
            if (entityCommandResult == null || !entityCommandResult.HasAProblem 
                                            || !_notificationContext.HasNotification) return entityCommandResult;
            var log = new StringBuilder();
            _notificationContext.Notifications.ToList().ForEach(error => {
                entityCommandResult.AddValidationError(error.Message);
                log.Append(error.Message);
            });
            _logger.LogError(log.ToString());
            return entityCommandResult;
        }

        protected abstract Task<EntityMapper<TEntity>> MapperCommandToEntityAsync(TCommand command);
        protected abstract Task ExecuteCommandAsync(TEntity entity);

        protected abstract object ReturnData(TEntity entity);
        
    }
}