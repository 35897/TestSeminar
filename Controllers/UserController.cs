using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestSeminar.Models;
using TestSeminar.Models.Binding;
using TestSeminar.Models.Dbo;
using TestSeminar.Services.Interface;

namespace TestSeminar.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserController(IUserService userService, SignInManager<ApplicationUser> signInManager)
        {
            this.userService = userService;
            this.signInManager = signInManager;
        }

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(ApplicationUserBinding model)
        {
            var result = await userService.CreateUserAsync(model,Roles.BasicUser);
            if(result != null)
            {
                await signInManager.SignInAsync(result, true);
                return RedirectToAction("Index","Home");
            }

            return View();
        }

    }
}
