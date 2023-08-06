using Microsoft.AspNetCore.Mvc;
using MVC_auth_app.Models;

namespace MVC_auth_app.Controllers
{
    public class HomeController : Controller
    {
        List<User> userList = new List<User>() 
        { 
            new User() { Id=1,Username="serhii", Password ="1234",IsAdmin=true},
            new User() { Id=2,Username="volodya337", Password ="0000",IsAdmin=false }
        };

        [Route("/")]
        [Route("/login")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/login")]
        [HttpPost]
        public IActionResult Index(User user)
        {
            User? wantedUser = userList.FirstOrDefault(x => x.Username == user.Username && x.Password.Equals(user.Password));
            if (wantedUser != null)
            {
                return RedirectToAction("HomePage", wantedUser);
            }            
            ViewBag.NonExistingAccount = "Invalid username or password!";
            return View();
        }

        [Route("/home")]
        public IActionResult HomePage(User user)
        {
            return View(user);
        }
    }
}
