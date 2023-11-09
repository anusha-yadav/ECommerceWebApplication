namespace E_Commerce_WebApplication.Utilities
{
    public class UserIdUtility : IUserIdUtility
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdUtility(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetUserId()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("userid");
        }
    }
}
