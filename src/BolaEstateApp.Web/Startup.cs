using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolaEstateApp.Data;
using BolaEstateApp.Data.DatabaseContexts.ApplicationDbContext;
using BolaEstateApp.Data.DatabaseContexts.AuthenticationDbContext;
using BolaEstateApp.Data.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BolaEstateApp.Web.Services;
using BolaEstateApp.Web.Interfaces;

namespace BolaEstateApp.Web {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            //this services.AddDbContextPool<AuthenticationDbContext> , says add a DbContext called the ApplicationDbContext
            //the sqlServerOptions is vey powerful
            //and we just used it , to say, when creating the database migration for the AuthenticationDbContext, create it inside BolaEstateApp.Data
            // which is sqlServerOptions => {
            // sqlServerOptions.MigrationsAssembly("BolaEstateApp.Data");
            //Configuration.GetConnectionString("AuthenticationConnection") this is connect it to  the database in the appsettings.json
            //this AuthenticationConnection is the name of the connection created in the appsettings.json for user authentication,so inside the bracket
            //here Configuration.GetConnectionString("AuthenticationConnection"),it has to tally with the name in the connection inside the appsettings.json file

            services.AddDbContextPool<AuthenticationDbContext> (
                options => options.UseSqlServer (Configuration.GetConnectionString ("AuthenticationConnection"),
                    sqlServerOptions => {

                        sqlServerOptions.MigrationsAssembly ("BolaEstateApp.Data");
                    }

                ));

            //in here , we want to make sure all our database are in the BolaEstateApp.Data folder
            //this services.AddDbContextPool<ApplicationDbContext> , says add a DbContext called the ApplicationDbContext
            //the sqlServerOptions is vey powerful
            //and we just used it , to say, when creating the database migration for ApplicationDbContext, create it inside BolaEstateApp.Data
            // which is sqlServerOptions => {
            // sqlServerOptions.MigrationsAssembly("BolaEstateApp.Data");
            //Configuration.GetConnectionString("ApplicationConnection") this is connect it to  the database in the appsettings.json,so inside the bracket
            //here Configuration.GetConnectionString("ApplicationConnection"),it has to tally with the name in the connection inside the appsettings.json file

            //this ApplicationConnection is the name of the connection created in the appsettings.json for our Application

            services.AddDbContextPool<ApplicationDbContext> (
                options => options.UseSqlServer (Configuration.GetConnectionString ("ApplicationConnection"),

                    sqlServerOptions => {

                        sqlServerOptions.MigrationsAssembly ("BolaEstateApp.Data");
                    }

                ));

            //WE NEED TO REGISTER IDENTITY
            // THIS IDENTITY IS USE TO IDENTIFY EACH OF THE INPUT BY THE USERS

            //ALL THIS options.Password.RequiredDigit ,IS CONFIGURING THE PASSWORD 
            //SAYING THE DIGIT IS NOT ALOWED, THAT IS WHY WE SENT IT TO FALSE
            //IF DIGIT IS ALLOWED IN PASSWORD, WE WILL SENT IT TO TRUE
            //options.Password.RequiredLength, that means the length of the password
            // options.Password.RequiredNonAlphanumeric = false; does not require alpha numeric
            //options.Password.RequiredLowercase = false;  does not require lower case
            //  options.Password.RequiredUppercase = false; does not require upper case
            //  options.SignIn.RequiredConfirmedEmail = false; if you want user to receive confirmation email after login
            //IF you want the user to receive confirmation message after login, then set to true

            //NOTE IF YOU SENT ALL OF THIS LIKE THIS, IT WEAKENS THE PASSWORD AND LOGIN STREGNTH
            //WHICH MKES THE APPLICATION VUNERABLE

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {

                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.AddControllersWithViews();
            services.AddTransient<IAccountsService, AccountsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider svp) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            app.UseHttpsRedirection ();
            app.UseStaticFiles ();

            app.UseRouting ();
            app.UseAuthentication();
            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllerRoute (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //calling the MigrateDatabaseContexts, that runs the migration 
            //after the application has being deployed to clouda

            MigrateDatabaseContexts (svp);

            //TO CALL THE CREATE USERS AND ROLES

            CreateDefaultRolesAndUsers (svp).GetAwaiter ().GetResult ();
        }

        // we are doing the below, so that when we are on our system, we can run this command to run 
        //to run our databae from the terminal
        //so that when we deploy to the cloud, it possible to do the igration
        //we can connect our database with the terminal when e are on the cloud
        //basically, to allow us run new migraton from the terminal, when our application is being deployed to the cloud

        public void MigrateDatabaseContexts (IServiceProvider svp) {
            //we arei=ggg grabbing a reference for authentication db Context

            var authenticationDbContext = svp.GetRequiredService<AuthenticationDbContext> ();

            //this simply checks our database and compare our migration
            //if we have not run any of it before
            //and if it does not find ny
            //it automatically run it
            //so we don't have to do it manually
            authenticationDbContext.Database.Migrate ();

            // we do the same for our application.db context too
            //so that we can run the samemigration for it too

            var applicationDbContext = svp.GetRequiredService<ApplicationDbContext> ();
            applicationDbContext.Database.Migrate ();

        }

        //under here , we are creating default users and different roles
        //cos different users with different roles
        //some users are going to be, house agent, some are going to be the people seeking for house
        //system administrators
        //so they have different roles
        //agent can upload and advertise, some are tenant looking for house
        //Basically for default users nd default roles

        public async Task CreateDefaultRolesAndUsers (IServiceProvider svp) {

            //create an array of strong to store, the list of users you want to have on the site
            //SystemAdministrator, Agent, User(which mean the person that wants to rent house)

            string[] roles = new string[] { "SystemAdministrator", "Agent", "User" }; //so this are all the users

            // store admin user name/email in  variable
            var userEmail = "admin@bolaestateapp.com";

            // store admin password in  variable
            var userPassword = "SuperSecretPassword@2020";

            // creating a rolemanager, to help us manage all our role
            // the rolemanager helps us to manage everything related to all roles
            var roleManager = svp.GetRequiredService<RoleManager<IdentityRole>> ();

            // we create a foreach loop to be able to loop through each role manager

            foreach (var role in roles) {
                // to check if the role exist we use RoleExistsAsync
                //this going to be true or false things
                //if therole exists , it will be TRUE
                //but if the role does not exist, it will be FALSE

                var roleExists = await roleManager.RoleExistsAsync(role);

                //the below means, if the role does not exist
                //then create the role
                //when creating a new role,we don't just pass the name of the new role
                //we have to pass in a new role instance like an object
                //an set the name of that object role
                //which is what we did here await roleManager.CreateAsync(new IdentityRole{ name = role });

                if (!roleExists) {
                    await roleManager.CreateAsync (new IdentityRole { Name = role });
                }
            }

            // creating a usermanager, to help us manage all user
            // the usermanager helps us to manage everything related to all users
            var userManager = svp.GetRequiredService<UserManager<ApplicationUser>>();

            //so we need to check if a user exists, before we can create the user into database
            //we checked if their is any userEmail which is  admin@bolaestateapp.com in our database
            //cos admin@bolaestateapp.com is our default admin users

            //NOTE THE BELOW, WE JUST CREATED USER FOR EXAMPLE

            var user = await userManager.FindByEmailAsync("userEmail");

            if (user is null) {
                user = new ApplicationUser 
                {

                    Email = userEmail,
                    UserName = userEmail,
                    EmailConfirmed = true,
                    PhoneNumber = "+2348032716181",
                    PhoneNumberConfirmed = true
                };

                //we are saying it should create the user
                await userManager.CreateAsync (user, userPassword);

                //ADD TO ROLES
                await userManager.AddToRolesAsync (user, roles);

            }

        }
    }
}