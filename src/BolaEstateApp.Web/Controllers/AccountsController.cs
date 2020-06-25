using System;
using BolaEstateApp.Web.Models;
using Microsoft.AspNetCore.Mvc;


namespace BolaEstateApp.Web.Controllers
{
    //the  AccountsController inherit (:) from   Controller
    //all your new controller creaated always inherit from Controller
    public class AccountsController : Controller
    {
        //so lets create the class  to return the view of login an register
        
        //this returns the login view(from from the html to the browser)
        [HttpGet]
        public IActionResult Login()

        {
            return View();
        }

     //creating action for the LoginModel
         [HttpPost]
        public IActionResult Login(LoginModel model)
          {
            if (!ModelState.IsValid) return View();
            
              throw new NotImplementedException();
          }


        //this returns the registration view(from from the html to the browser)
         [HttpGet]
         public IActionResult Register()

        {
            return View();
        }


          [HttpPost]
        public IActionResult Register(RegisterModel model)
          {
            if (!ModelState.IsValid) return View();
            
              throw new NotImplementedException();
          }

    }
}