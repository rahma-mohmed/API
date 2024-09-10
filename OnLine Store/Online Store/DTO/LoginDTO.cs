using System.ComponentModel.DataAnnotations;

namespace WebAPI2.DTO
{
    public class LoginDTO
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
