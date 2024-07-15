using System.Security.Claims;
using MextFullstackSaaS.Application.Common.Interfaces;

namespace MextFullstackSaaS.WebApi.Services
{
    public class CurrentUserManager:ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // public Guid UserId => new("35c16d2a-f25b-4701-9a74-ea1fb7ed6d93");
        public Guid UserId => GetUserId();
          public string? UserName => GetUserName();

        public string? FullName => GetFullName();

        private Guid GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");

            return userId is null ? Guid.Empty : Guid.Parse(userId);
        }
            private string? GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        }

        private string? GetFullName()
        {
            var firstName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.GivenName);
            var lastName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Surname);

            if (firstName is null && lastName is null) return null;

            return $"{firstName} {lastName}".Trim();
        }

    }
}
