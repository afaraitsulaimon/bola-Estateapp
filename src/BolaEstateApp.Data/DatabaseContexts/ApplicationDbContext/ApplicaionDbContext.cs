


namespace BolaEstateApp.Data.DatabaseContexts.ApplicationDbContext
{
    
//i inherited only DbContext here, because i don't need to manage user table here
    public class ApplicationDbContext : DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
       {

       }
        
    //in here,this the place to tel our application,that the details n the 
    //contact.cs and property.cs is going into the database
// this DbSet<Property> Properties, means when the table that is going to hold
//the property details from the proprty.cs is going to be named Propertiess
//the same applies to contact.cs also
//but table for Properties and Contacts will be created in the database
    
    public DbSet<Property> Properties {get; set;}
    public DbSet<Contact> Contacts {get; set;}
  
    }
}