using Microsoft.AspNetCore.Identity;
using WebAPI2.DTO;

namespace WebAPI2.IRepository
{
    public interface IUserRepo
    {
        public Task<object> Login(LoginDTO UserFromRweq);
        public Task<IdentityResult> Register(RegisterDTO userFromReq);
    }
}
