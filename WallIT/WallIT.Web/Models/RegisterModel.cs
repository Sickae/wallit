using System.ComponentModel;
using WallIT.Shared.DTOs;

namespace WallIT.Web.Models
{
    public class RegisterModel
    {
        public UserDTO User { get; set; }

        // TODO localization
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Confirm password")]
        public string PasswordRe { get; set; }
    }
}
