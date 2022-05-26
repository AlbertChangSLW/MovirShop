using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var user = await _accountService.LoginUser(model.Email, model.Password);
            var jwtToken = GenerateJwtToken(user);
            return Ok(new {token = jwtToken});

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if(ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _accountService.RegisterUser(model); 
            return Ok(model);
        }

        private string GenerateJwtToken(UserLoginResponseModel user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Language", "English")
            };
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var indentityClaim = new ClaimsIdentity();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyTopSecretKeyIsUseJwtHmacSha256ToEncrypt"));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenExpiration = DateTime.UtcNow.AddHours(2);
            var tokenHeadler = new JwtSecurityTokenHandler();
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = tokenExpiration,
                SigningCredentials = credentials,
                Issuer = "MovieShop, Inc",
                Audience = "MovieShop User"
            };

            var encodedJwt = tokenHeadler.CreateToken(tokenDetails);
            return tokenHeadler.WriteToken(encodedJwt);
        }
    }
}
