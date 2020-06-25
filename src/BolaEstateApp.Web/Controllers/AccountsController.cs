using Microsoft.AspNetCore.Mvc;


namespace BolaEstateApp.Web.Controllers
{
    //the  AccountsController inherit (:) from   Controller
    //all your new controller creaated always inherit from Controller
    public class AccountsController : Controller
    {
        //so lets create the class  to return the view of login an register
        
        //this returns the login view(from from the html to the browser)
        public IActionResult Login()

        {
            return View();
        }


        //this returns the registration view(from from the html to the browser)

         public IActionResult Register()

        {
            return View();
        }

    }
}