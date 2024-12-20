using System.Security.Claims;

namespace Organizations.API.Common.Accessors
{
    public interface IUserAccessor
    {
        string GetUserId();
        string GetUserName();
        string GetUserEmail();
        string GetUserFirstName();
        string GetUserLastName();
    }

    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        }

        public string GetUserEmail()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        }

        public string GetUserFirstName()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value;
        }

        public string GetUserLastName()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Surname)?.Value;
        }
    }
}