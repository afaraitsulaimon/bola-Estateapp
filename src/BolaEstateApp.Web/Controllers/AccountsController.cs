using System;
using BolaEstateApp.Data.Entities;
using BolaEstateApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using BolaEstateApp.Web.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Text;



namespace BolaEstateApp.Web.Controllers
{
    //the  AccountsController inherit (:) from   Controller
    //all your new controller creaated always inherit from Controller
    public class AccountsController : Controller
    {

        private readonly IAccountsService _accountsService;
        private readonly SignInManager<ApplicationUser> _signInManager;


    //here we created a  constructor
    //you can create constructor by using the name of the controller
    //like we did below , doing AccountsController, but won't return anything
    //and we injected an IAccountAervice into it
    //and above we created a private class to store it which is private readonly IAccountsService _accountsService;
        public AccountsController(
            IAccountsService accountsService,
            SignInManager<ApplicationUser> signInManager)
        {
        //this are both saved based on the private read ony created above for both created account and the sign in user
            _accountsService = accountsService;
            _signInManager = signInManager;

        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("~/");
        }
        //so lets create the class  to return the view of login an register
        
        //this returns the login view(from from the html to the browser)
        [HttpGet]
        public IActionResult Login()

        {
            return View();
        }

     //creating action for the LoginModel
         [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
          {
            if (!ModelState.IsValid) return View();

            try{
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (!result.Succeeded)
                {
                    
                    ModelState.AddModelError("", "Login failed, please check your details");
                    return View();
                }

                return LocalRedirect("~/");
            }

            catch(Exception e){

                ModelState.AddModelError("", e.Message);
                     return View();
            }
            
            
              
          }


        //this returns the registration view(from from the html to the browser)
         [HttpGet]
         public IActionResult Register()

        {
            return View();
        }


          [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
          {
            if (!ModelState.IsValid) return View();
            
              try
              {
                  var user = await _accountsService.CreateUserAsync(model);

                  //this below code allows the user to be logged in automatically after registration
                  await _signInManager.SignInAsync(user, isPersistent: false);

                  //the below returns user to our homepage after registered and logged in automaticallyss
                  return LocalRedirect("~/");
              }
              catch(Exception e)
                 {
                     ModelState.AddModelError("", e.Message);
                     return View();
                 }
          }

          
    }
}