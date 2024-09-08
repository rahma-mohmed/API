using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> Usermanager;
        private readonly IConfiguration config;

        public AccountController(UserManager<User> usermanager , IConfiguration configure)
        {
            Usermanager = usermanager;
            config = configure;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO userFromReq)
        {
            if (ModelState.IsValid) {
                User user = new User();
                user.UserName = userFromReq.UserName;
                user.Email = userFromReq.Email;
                user.PasswordHash = userFromReq.Password;
                user.PhoneNumber = userFromReq.PhoneNumber;
                

                IdentityResult res =  await Usermanager.CreateAsync(user , userFromReq.Password);

                if (res.Succeeded) {
                    return Ok(res);
                }
                else
                {
                    foreach (var error in res.Errors) {
                        ModelState.AddModelError("Errors", error.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO userFromReq)
        {
            if (ModelState.IsValid)
            {
                //check - token
                User user = await Usermanager.FindByNameAsync(userFromReq.Name);

                if(user != null)
                {
                    bool found = await Usermanager.CheckPasswordAsync(user, userFromReq.Password);

                    if (found)
                    {
                        List<Claim> UserClaims = new List<Claim>();
                        UserClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));   
                        UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())); 

                        var roles = await Usermanager.GetRolesAsync(user);

                        foreach (var role in roles) {
                            UserClaims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecurityKey"]));

                        SigningCredentials signingCredentials = new SigningCredentials(Key , SecurityAlgorithms.HmacSha256);

                        //desig Token
                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: config["JWT:IssuerIP"],
                            audience: config["JWT:AudienceIP"],
                            expires: DateTime.Now.AddHours(1),
                            claims: UserClaims,
                            signingCredentials: signingCredentials
                            );
                        //generate token
                        return Ok(
                            new
                            {
                                token =  new JwtSecurityTokenHandler().WriteToken(token),
                                expire = DateTime.Now.AddHours(1) //token.ValidTo
                            }
                            );
                    }

                }
                ModelState.AddModelError("Errors", "User name or password invalid");
            }
            return BadRequest(ModelState);
        }
    }
}
