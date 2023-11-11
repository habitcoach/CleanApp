using Clean.Application.DTOs;
using Clean.Infra.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clean.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {

                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username

            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                if (identityResult.Succeeded)
                {

                    return Ok("user is registered.  Please login");

                }

            }

            return BadRequest("Something went wrong");

        }


        [HttpPost("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {

            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult) //Check this in swagger before adding token module
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {

                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {

                            JwtToken = jwtToken,

                        };
                        return Ok(response);
                    }

                }
            }

            return BadRequest("Username or password incorrect");

        }



    }
}
