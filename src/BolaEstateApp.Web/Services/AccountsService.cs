using System;
using BolaEstateApp.Data.Entities;
using BolaEstateApp.Web.Models;
using BolaEstateApp.Web.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;



namespace BolaEstateApp.Web.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsService(
            UserManager<ApplicationUser> userManager)
            {

            _userManager = userManager;

        }
        //this create a new user for us
        public async Task<ApplicationUser> CreateUserAsync(RegisterModel model)

        {
            // this below code, actualy says, if the user is not among the user
            //or trying to break into the login, then throw an error invalid details provided
            if(model is null) throw new ArgumentNullException(message:"Invalid details provided", null);

            //then start to crete new users
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(message: AddErrors(result), null);
                
            }
            return user;

        }

        private string AddErrors(IdentityResult result)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var error in result.Errors)
            {
               sb.Append(error.Description + ""); 
            }
            return sb.ToString();
        }
        
    }
}