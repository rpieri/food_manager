using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace FoodManager.SharedKernel.Domain.Models
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = new Guid();
            CreatedDate = DateTime.UtcNow;
            Valid = true;
        }
        public Guid Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? RemovedDate { get; private set; }
        public DateTime? LastUpdateDate { get; private set; }
        public Guid TenantId { get; private set; }
        public bool Removed { get; private set; }

        public void Remove()
        {
            Removed = true;
            RemovedDate = DateTime.UtcNow;
        }

        public void SetCompanyCode(Guid companyCode) => TenantId = companyCode;

        public void Update() => LastUpdateDate = DateTime.UtcNow;

        #region Validation
        public bool Valid { get; private set; }
        public ValidationResult ValidationResult { get; private set; }
        public bool Invalid => !Valid;

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator) where TModel : Entity
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }
        #endregion
        
        #region Comparative
        protected abstract IEnumerable<object> GetEqualityComponents();

        public bool Equal(Entity obj) => this == obj;
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (!(obj is Entity)) return false;

            return this == (obj as Entity);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 30;

                foreach (var item in GetEqualityComponents())
                {
                    hash = HashCode.Combine(hash, item) * 31;
                }

                return hash;
            }
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            return a.GetType() == b.GetType() && a.TenantId == b.TenantId &&
                   a.GetEqualityComponents().SequenceEqual(b.GetEqualityComponents());
        }

        public static bool operator !=(Entity a, Entity b) => !(a == b);
        #endregion
    }
}