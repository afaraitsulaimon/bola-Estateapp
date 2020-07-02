using Microsoft.AspNetCore.Identity;


namespace BolaEstateApp.Data.Entities
{
    public class ApplicationUser : IdentityUser

    {
       public string FullName{get; set; }
    }
}