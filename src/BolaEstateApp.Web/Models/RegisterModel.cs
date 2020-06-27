using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BolaEstateApp.Web.Models
{
    public class RegisterModel
    {
         //this 4 (Name ,Email ,Password and confirm password)are what we expect back from our registration form from our users
        [DisplayName("Name")]
         [Required]
      public string FullName { get; set; }

        [DisplayName("Email Address")]
        [Required]
        [EmailAddress]
      public string Email { get; set; }

      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [Required]
      [Compare(nameof(Password))]
      public string ConfirmPassword { get; set; }
    }
}