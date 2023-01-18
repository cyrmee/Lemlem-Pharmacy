using LemlemPharmacy.DTOs;
using LemlemPharmacy.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace LemlemPharmacy.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Authorize(Roles = $"{UserRole.Manager}")]
	[ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
		private readonly string pattern = @"(\+\s*2\s*5\s*1\s*9\s*(([0-9]\s*){8}\s*))|(0\s*9\s*(([0-9]\s*){8}))";

		public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<UserInfoDTO>>> GetAllUsers()
		{
            var result = await userManager.Users.ToListAsync();

			if (result == null) return NotFound();

			var users = new List<UserInfoDTO>();
			foreach (var item in result)
            {
				string userRole = string.Empty;
				var userRoles = await userManager.GetRolesAsync(item);
				foreach (var role in userRoles)
					userRole = role;
				users.Add(new UserInfoDTO(item, userRole));
			}

			return users;
		}

		[HttpGet("userName/{userName}")]
		public async Task<ActionResult<UserInfoDTO>> GetUserByUserName(string userName)
		{
            var result = await userManager.Users.ToListAsync();

			if (result == null) return NotFound(new Response()
            {
                Status = "Error",
                Message = $"{userName} not found"
            });
			
			foreach (var item in result)
			{
				string userRole = string.Empty;
				var userRoles = await userManager.GetRolesAsync(item);
				foreach (var role in userRoles)
					userRole = role;
				if (item.UserName.Contains(userName))
					return new UserInfoDTO(item, userRole);
			}

			return NotFound(new Response()
			{
				Status = "Error",
				Message = $"{userName} not found"
			});
		}

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<UserInfoDTO>>> GetUserByName(string name)
        {
            var result = await userManager.Users.ToListAsync();

            if (result == null) return NotFound();

            var users = new List<UserInfoDTO>();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].NormalizedUserName = result[i].NormalizedUserName.ToLower();
            }
            foreach (var item in result)
            {
				string userRole = string.Empty;
				var userRoles = await userManager.GetRolesAsync(item);
				foreach (var role in userRoles)
					userRole = role;
				if (item.NormalizedUserName.Contains(name.ToLower()))
					users.Add(new UserInfoDTO(item, userRole));
			}

            return users;
        }

		[HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LogInDTO model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var UserRole = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in UserRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    audience: _configuration["JWT:Audience"],
                    issuer: _configuration["JWT:Issuer"],
                    claims: authClaims,
                    expires: DateTime.UtcNow.AddDays(3D),
                    signingCredentials: credentials);


                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
		[Authorize(Roles = $"{UserRole.Manager}, {UserRole.Pharmacist}")]
		[Route("changepassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (!result.Succeeded) { return BadRequest(result); }

                return Ok();
            }
            return Unauthorized();
        }

		[HttpPost]
		[Authorize(Roles = $"{UserRole.Manager}")]
		[Route("updatePassword")]
		public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordDTO model)
		{
			var user = await userManager.FindByNameAsync(model.UserName);
			var userRole = await userManager.GetRolesAsync(user);
			if (user != null && !userRole.Contains(UserRole.Manager))
			{
				user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.NewPassword);
				var result = await userManager.UpdateAsync(user);
				if (result.Succeeded)
				{
					return Ok(new Response()
					{
						Status = "Success",
						Message = $"Password updated for user {model.UserName}"
					});
				}
			}
			return Unauthorized();
		}

		[HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDTO registerUser)
        {
            try
            {
				var userExists = await userManager.FindByNameAsync(registerUser.UserName);
				if (userExists != null)
					return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
				ApplicationUser user;
				if (Regex.IsMatch(registerUser.PhoneNo, pattern))
				{
					user = new ApplicationUser()
					{
						Email = registerUser.Email,
						SecurityStamp = Guid.NewGuid().ToString(),
						UserName = registerUser.UserName,
						PhoneNumber = registerUser.PhoneNo
					};
				}
				else return BadRequest(new Response()
				{
					Status = "Error",
					Message = "Phone number not in the right format. Example: +251 91 234 5678 +251912345678"
				});

				var result = await userManager.CreateAsync(user, registerUser.Password);

				registerUser.Role = registerUser.Role.ToLower();
				if (!await roleManager.RoleExistsAsync(UserRole.Pharmacist))
					await roleManager.CreateAsync(new IdentityRole(UserRole.Pharmacist));

				if (!await roleManager.RoleExistsAsync(UserRole.Manager))
					await roleManager.CreateAsync(new IdentityRole(UserRole.Manager));

				if (registerUser.Role == null)
					return BadRequest(new Response()
					{
						Status = "Error",
						Message = "Please choose a role!"
					});
				else if (registerUser.Role == UserRole.Pharmacist)
					await userManager.AddToRoleAsync(user, UserRole.Pharmacist);
				else if (registerUser.Role == UserRole.Manager)
					await userManager.AddToRoleAsync(user, UserRole.Manager);

				if (!result.Succeeded)
					return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

				return Ok(new Response { Status = "Success", Message = "User created successfully!" });
			}
            catch(Exception e)
            {
				return BadRequest(new Response { Status = "Error", Message = e.Message });
			}
        }
    }
}