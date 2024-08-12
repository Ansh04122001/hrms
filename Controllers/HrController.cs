using Hrms.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hrms.Controllers
{
    [Authorize(Roles = "hr")]
    public class HrController : Controller
    {

        private readonly AppDbContext _dbContext;

        public HrController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult HrProfile()
        {
            string username = User.Identity.Name;
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == username);

            return View(employee);
        }
    }
}
