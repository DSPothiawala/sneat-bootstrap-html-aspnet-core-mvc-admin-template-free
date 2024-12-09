using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username is required.")]
        [MaxLength(20,ErrorMessage ="Max 20 characters allowed.")]
        [DisplayName("Username or Email")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
