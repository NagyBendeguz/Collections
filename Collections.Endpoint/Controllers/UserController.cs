using Collections.Entities.Dtos.User;
using Collections.Logic.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Collections.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        DtoProvider dtoProvider;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, DtoProvider dtoProvider)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dtoProvider = dtoProvider;
        }

        [HttpGet("grantadmin/{userid}")]
        [Authorize(Roles = "Admin")]
        public async Task GrantAdmin(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("Incorrect username or password!");
            }
            await userManager.AddToRoleAsync(user, "Admin");
        }

        [HttpGet("revokeadmin/{userid}")]
        [Authorize(Roles = "Admin")]
        public async Task RevokeAdmin(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("Incorrect username or password!");
            }

            var adminCount = (await userManager.GetUsersInRoleAsync("Admin")).Count();

            if (adminCount > 1)
            {
                await userManager.RemoveFromRoleAsync(user, "Admin");
            }
            else
            {
                throw new ArgumentException("Minimum one admin is required!");
            }
        }

        [HttpGet]
        public IEnumerable<UserViewDto> GetUsers()
        {
            return userManager.Users.Select(t =>
                dtoProvider.Mapper.Map<UserViewDto>(t)
            );
        }

        [HttpPost("register")]
        public async Task Register(UserInputDto dto)
        {
            var user = new IdentityUser(dto.UserName);
            await userManager.CreateAsync(user, dto.Password);
            if (userManager.Users.Count() == 1)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserInputDto dto)
        {
            var user = await userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                throw new ArgumentException("Incorrect username or password!");
            }
            else
            {
                var result = await userManager.CheckPasswordAsync(user, dto.Password);
                if (!result)
                {
                    throw new ArgumentException("Incorrect username or password!");
                }
                else
                {
                    var claim = new List<Claim>();
                    claim.Add(new Claim(ClaimTypes.Name, user.UserName!));
                    claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

                    foreach (var role in await userManager.GetRolesAsync(user))
                    {
                        claim.Add(new Claim(ClaimTypes.Role, role));
                    }

                    int expiryInMinutes = 48 * 60;
                    var token = GenerateAccessToken(claim, expiryInMinutes);

                    return Ok(new LoginResultDto()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = DateTime.Now.AddMinutes(expiryInMinutes)
                    });
                }
            }
        }

        private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim>? claims, int expiryInMinutes)
        {
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OfED+KgbZxtu4e4+JSQWdtSgTnuNixKy1nMVAEww8QL3IN3idcNgbNDSSaV4491Fo3sq2aGSCtYvekzs7JwXJnNAyvDSJjfK/7M8MpxSMnm1vMscBXyiYFXhGC4wqWlYBE828/5DNyw3QZW5EjD7hvDrY5OlYd4smCTa53helNnJz5NT9HQaDbE2sMwIDAQABAoIBAEs63TvT94njrPDP3A/sfCEXg1F2y0D/PjzUhM1aJGcRiOUXnGlYdViGhLnnJoNZTZm9qI1LT0NWcDA5NmBN6gcrk2EApyTt1D1i4AQ66rYoTF9iEC4Wye28v245BYESA6IIelgIxXGsVyllERsbTkaphzibbYfHmvwMxkn135Zfzd/NOXl/O32vYIomzrNEP+tN2WXhhG8c8+iZ8PErBV3CqrYogYy97d2CeQbXcpd5unPiU4TK0nnzeBAXdgeYuJHFC45YHl9UvShRoe6CHR47ceIGp6WMc5BTyyTkZpctuYJTwaChdj/QuRSkTYmn6jFL+MRfYQJ8VVwSVo5DbkECgYEA4/YIMKcwObYcSuHzgkMwH645CRDoy9M98eptAoNLdJBHYz23U5IbGL1+qHDDCPXxKs9ZG7EEqyWezq42eoFoebLA5O6/xrYXoaeIb094dbCF4D932hAkgAaAZkZVsSiWDCjYSV+JoWX4NVBcIL9yyHRhaaPVULTRbPsZQWq9+hMCgYEA48j4RGO7CaVpgUVobYasJnkGSdhkSCd1VwgvHH3vtuk7/JGUBRaZc0WZGcXkAJXnLh7QnDHOzWASdaxVgnuviaDi4CIkmTCfRqPesgDR2Iu35iQsH7P2/o1pzhpXQS/Ct6J7/GwJTqcXCvp4tfZDbFxS8oewzp4RstILj+pDyWECgYByQAbOy5xB8GGxrhjrOl1OI3V2c8EZFqA/NKy5y6/vlbgRpwbQnbNy7NYj+Y/mV80tFYqldEzQsiQrlei78Uu5YruGgZogL3ccj+izUPMgmP4f6+9XnSuN9rQ3jhy4k4zQP1BXRcim2YJSxhnGV+1hReLknTX2IwmrQxXfUW4xfQKBgAHZW8qSVK5bXWPjQFnDQhp92QM4cnfzegxe0KMWkp+VfRsrw1vXNx"));

            return new JwtSecurityToken(
                issuer: "Collections",
                audience: "Collectors",
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryInMinutes),
                signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
