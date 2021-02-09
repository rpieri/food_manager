using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace FoodManager.SharedKernel.Domain.Notifications
{
    public sealed class NotificationContext
    {
        private readonly List<Notification> _notifications;

        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public IReadOnlyCollection<Notification> Notifications => _notifications.ToList();
        public bool HasNotification => _notifications.Any();
        public void AddNotification(string key, string message) => _notifications.Add(new Notification(key, message));

        public void AddNotification(ValidationResult validationResult)
            => validationResult.Errors.ToList().ForEach(e => AddNotification(e.ErrorCode, e.ErrorMessage));
    }
}