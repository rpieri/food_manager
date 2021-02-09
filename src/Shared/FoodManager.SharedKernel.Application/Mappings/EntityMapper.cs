﻿using FoodManager.SharedKernel.Domain.Models;

namespace FoodManager.SharedKernel.Application.Mappings
{
    public sealed class EntityMapper<TEntity> where TEntity : Entity
    {
        public EntityMapper(bool success, string message = "", TEntity entity = null)
        {
            Success = success;
            Message = message;
            Entity = entity;
        }

        public bool Success { get; private set; }
        public string Message { get; private set; }
        public TEntity Entity { get; private set; }
        public bool HasAProblem => !Success;
    }
}