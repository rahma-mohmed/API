using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI2.DTO;
using WebAPI2.IRepository;
using WebAPI2.Model;

namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUserRepo userRepo,UserManager<User> userManager , RoleManager<IdentityRole> roleManager)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _roleManager = roleManager; 
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO userFromReq)
        {
            if (ModelState.IsValid) {
                IdentityResult res = await _userRepo.Register(userFromReq);

                if (res.Succeeded)
                {
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
                object testLogin = await _userRepo.Login(userFromReq);

                if (testLogin != null)
                {
                    return Ok(testLogin);
                }
            }
            ModelState.AddModelError("Errors", "User name or password invalid"); 
            return BadRequest(ModelState);  
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(role));
                if (!roleResult.Succeeded)
                {
                    return BadRequest("Failed to create role");
                }
            }
            await _userManager.AddToRoleAsync(user, role);
            return Ok("Role assigned successfully");
        }

    }
}
