using System.ComponentModel.DataAnnotations;

namespace WebAPI2.DTO
{
    public class RegisterDTO
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
