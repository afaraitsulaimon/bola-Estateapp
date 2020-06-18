using System;
using BolaEstateApp.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace BolaEstateApp.Data.DatabaseContexts.ApplicationDbContext
{
    
//i inherited only DbContext here, because i don't need to manage user table here
    public class ApplicationDbContext : DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
       {

       }
   
    
    public DbSet<Property> Properties { get; set; }
    public DbSet<Contact> Contacts { get; set; }
  
    }
}