using System.Security.Claims;

namespace MovieShopMVC.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int? UserId => Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        public bool IsAdmin => throw new NotImplementedException();

        public bool IsAuthenticated => _contextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string Email => _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

        public string FullName => _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname).Value + " "
            + _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value;

        public IEnumerable<string> Roles => throw new NotImplementedException();
    }
}
