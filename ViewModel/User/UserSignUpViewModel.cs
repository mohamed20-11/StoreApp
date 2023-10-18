using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserSignUpViewModel
    {
        [Required ,StringLength(50,MinimumLength =3)]
        public string FirstName { get; set; }


        [Required, StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }


        [Required, StringLength(14, MinimumLength = 14)]
        public string NationalID { get; set; }

        public string Picture { get; set; } = "notfound.png";

        [Required, StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required, StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }


        [Required, StringLength(15, MinimumLength = 11)]
        public string PhoneNumber { get; set; }


        [Required, StringLength(20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required, StringLength(20, MinimumLength = 8)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; } = "User";
    }
}
