
using Hrms.Models.DTO;
using Hrms.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hrms.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _authService;
        public UserAuthenticationController(IUserAuthenticationService authService)
        {
            this._authService = authService;
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Display", "Admin");
            }
            if (result.StatusCode == 2)
            {
                return RedirectToAction("Display", "Dashboard");
            }
            if (result.StatusCode == 3)
            {
                return RedirectToAction("Dashboard", "Manager");
            }
            if (result.StatusCode == 4)
            {
                return RedirectToAction("Dashboard", "Hr");
            }
            else
            {
                TempData["msg"] = result.Message;
                return View(model); 
            }
        }




        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid) { return View(model); }
            model.Role = "user";
            var result = await this._authService.RegisterAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
        }
        [HttpPost]
        public async Task<IActionResult> EmpRegistration(Emp employee)
        {
            if (!ModelState.IsValid) { return View(employee); }
           
            var result = await this._authService.EmpAsync(employee, ModelState);
            TempData["msg"] = result.Message;
            return RedirectToAction("Employee", "Admin");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAdmin()
        {
            RegistrationModel model = new RegistrationModel
            {
                Username = "admin",
                Email = "admin@razorse.com",
                FirstName = "Ansh",
                LastName = "Bhargava",
                Password = "Admin@123"
            };
            model.Role = "admin";
            var result = await this._authService.RegisterAsync(model);
            return Ok(result);
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.ChangePasswordAsync(model, User.Identity.Name);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(ChangePassword));
        }
        public IActionResult HolidayList()
        {
            return View();
        }
    }
}
