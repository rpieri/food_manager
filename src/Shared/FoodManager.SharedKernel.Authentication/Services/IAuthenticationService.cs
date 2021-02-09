using System;

namespace FoodManager.SharedKernel.Authentication.Services
{
    public interface IAuthenticationService
    {
        void SetApplicationTenantId(Guid tenantId);
        Guid GetTenantId();
    }
}