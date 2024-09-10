using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI2.DTO;
using WebAPI2.IRepository;
using WebAPI2.Model;

namespace WebAPI2.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<User> _manager;
        private readonly IConfiguration _config;

        public UserRepo(UserManager<User> usermanager , IConfiguration config) {
            _manager = usermanager;
            _config = config;
        }

        public async Task<object> Login(LoginDTO userFromReq)
        {
            User user = await _manager.FindByNameAsync(userFromReq.UserName);

            if (user != null)
            {
                bool check = await _manager.CheckPasswordAsync(user, userFromReq.Password);

                if (check) {

                    List<Claim> claimsList = new List<Claim>();
                    claimsList.Add(new Claim(ClaimTypes.Name, user.UserName));
                    claimsList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    claimsList.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    var roles = await _manager.GetRolesAsync(user);

                    foreach (var role in roles)
                    {
                        claimsList.Add(new Claim(ClaimTypes.Role, role));
                    }

                    SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SiginingKey"]));

                    SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken token = new JwtSecurityToken(
                        issuer: _config["JWT:Issuer"],
                        audience: _config["JWT:Audience"],
                        expires: DateTime.Now.AddHours(1),
                        claims: claimsList,
                        signingCredentials: signingCredentials
                   );

                    return new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expire = DateTime.Now.AddHours(1) //token.ValidTo
                    };      
                }
            }
            return new { };
        }

        public async Task<IdentityResult> Register(RegisterDTO userFromReq)
        {
            User user = new User();
            user.UserName = userFromReq.UserName;
            user.Email = userFromReq.Email;
            user.PasswordHash = userFromReq.Password;
            user.PhoneNumber = userFromReq.PhoneNumber;

            IdentityResult result = await _manager.CreateAsync(user, userFromReq.Password);

            return result;
        }
    }
}
