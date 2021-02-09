using System;

namespace FoodManager.SharedKernel.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private Guid? _tenantId;
        public void SetApplicationTenantId(Guid tenantId) => _tenantId = tenantId;

        public Guid GetTenantId()
            =>  _tenantId ?? Guid.Empty;
    }
}