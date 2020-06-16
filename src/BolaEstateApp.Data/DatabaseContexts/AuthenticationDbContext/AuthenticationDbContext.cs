using BolaEstateApp.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BolaEstateApp.Data.DatabaseContexts.AuthenticationDbContext
{
       
//inherited from IdentityDbContext of Application user, 
//when entityFrameworkCore tries to run your migration 
//and sees this  IdentityDbContext<ApplicationUser>, 
//it knows that you need table for user management here
//that is why , when i runs the migration, it creates the table
//Because inherited from IdentityDbContext
//here i need to manage user table that is why i inherited from IdentityDbContext<ApplicationUser>

    public class AuthenticationDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
            : base(options)
        {
        }
    }
}