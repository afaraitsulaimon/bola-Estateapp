using System.ComponentModel.DataAnnotations;

namespace BolaEstateApp.Web.Models
{
    public class LoginModel
    {
        //this 2 (Email and Password)are what we expect back from our login form from our users
        [Required]
        [EmailAddress]
      public string Email { get; set; }

      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }
        
    }
}