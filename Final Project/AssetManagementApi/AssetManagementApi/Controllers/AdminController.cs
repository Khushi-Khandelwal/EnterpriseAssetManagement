using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetManagementApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]

	public class AdminController : ControllerBase
	{
		private readonly IConfiguration _config;
		private readonly IUser _userRepository;

		public AdminController(IConfiguration config , IUser userRepository)
		{
			_config = config;
			_userRepository = userRepository;
		}



        
        private string GenerateToken([FromRoute] string Email , [FromRoute] string password)
		{
			
			var user = _userRepository.GetAll().Where(i => i.Email == Email).FirstOrDefault();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier,user.Email),
				(!user.IsAdmin)? new Claim(ClaimTypes.Role,"User") : new Claim(ClaimTypes.Role,"Admin")
			};

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Audiance"],
          claims,
          expires: DateTime.Now.AddDays(1),

          signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

       


        [HttpPost]
        public IActionResult AdminLogin(Login admin)
        {
            try
            {
                if (_userRepository.AdminLogin(admin.Email, admin.Password))
                {
                    var token = GenerateToken(admin.Email, admin.Password);
                    return Ok(token);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost]
        public IActionResult UserLogin(Login user)
        {
            try
            {
                if (_userRepository.UserLogin(user.Email, user.Password))
                {
                    var token = GenerateToken(user.Email, user.Password);
                    return Ok(token);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
