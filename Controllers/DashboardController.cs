using Hrms.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hrms.Controllers
{
    [Authorize(Roles = "user")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _dbContext;

        public DashboardController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        public IActionResult Timesheet()
        {
          

            string username = User.Identity.Name;
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == username);

            // Assuming you have a ViewModel class for the Timesheet view
            var timesheetViewModel = new Timesheet();

            if (employee != null)
            {
                timesheetViewModel.EmployeeCode = employee.EmployeeCode;
                timesheetViewModel.EmployeeName = employee.FirstName;
                timesheetViewModel.ManagerName = employee.ManagerName;
                timesheetViewModel.Department = employee.DepartmentName;
            }

            return View(timesheetViewModel);
        }


        public IActionResult ApplyLeave()
        {


            string username = User.Identity.Name;
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == username);

            // Assuming you have a ViewModel class for the Timesheet view
            var leaveViewModel = new Leave();

            if (employee != null)
            {
                leaveViewModel.EmployeeCode = employee.EmployeeCode;
                leaveViewModel.EmployeeName = employee.FirstName ;
                leaveViewModel.ManagerName = employee.ManagerName;
                leaveViewModel.Department = employee.DepartmentName;
            }

            return View(leaveViewModel);
        }
        public IActionResult EmpProfile()
        {
            string username = User.Identity.Name;
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == username);

            return View(employee);
        }
        public IActionResult ViewTimesheet()
        {
            string username = User.Identity.Name;

            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == username);
            List<Emp> employees = _dbContext.Employees.ToList();
            List<string> firstNames = employees.Select(e => e.FirstName).ToList();
            ViewBag.Employees = new SelectList(firstNames);

            return View(employee);
        }

        public IActionResult GetEmployeeDetails(string name)
        {
            var employees = _dbContext.Timesheets.Where(e => e.EmployeeName == name).ToList();
         
            return Json(employees);
        }


        [HttpPost]
        public IActionResult AddTimeSheet(Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Timesheets.Add(timesheet);
                _dbContext.SaveChanges();
                return RedirectToAction("Display", "Dashboard");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userId)
        {
            var today = DateTime.UtcNow.Date;
            var existingLog = await _dbContext.UserLoginLogs
                .Where(x => x.UserId == userId && x.Date == today)
                .FirstOrDefaultAsync();

            if (existingLog != null)
            {
                return Json(new { success = false, message = "You have already logged in today." });
            }

            var userLog = new UserLoginLogs
            {
                UserId = userId,
                Date = today,
                LoginTime = DateTime.Now
            };
            _dbContext.UserLoginLogs.Add(userLog);
            await _dbContext.SaveChangesAsync();

            // Set login state in session
            HttpContext.Session.SetString("IsLoggedIn", "true");

            return Json(new { success = true, message = "Login time is recorded." });
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string userId)
        {
            var today = DateTime.UtcNow.Date;
            var userLog = await _dbContext.UserLoginLogs
                .Where(x => x.UserId == userId && x.Date == today && x.LogoutTime == null)
                .OrderByDescending(x => x.LoginTime)
                .FirstOrDefaultAsync();

            if (userLog == null)
            {
                return Json(new { success = false, message = "No login record found for today or you have already logged out." });
            }

            userLog.LogoutTime = DateTime.Now;
            userLog.TotalTimeLoggedIn = userLog.LogoutTime - userLog.LoginTime;
            await _dbContext.SaveChangesAsync();

            // Clear login state in session
            HttpContext.Session.Remove("IsLoggedIn");

            return Json(new { success = true, message = "Logout time is recorded." });
        }
        public IActionResult Display()
        {
            bool isLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true";
            ViewBag.IsLoggedIn = isLoggedIn;

            return View();
        }

        public IActionResult AttendanceView()
        {
            
            return View();
        }

        public IActionResult GetAttendance(string name)
        {
            var employees = _dbContext.UserLoginLogs.Where(e => e.UserId == name).ToList();

            return Json(employees);
        }
        [HttpPost]
        public IActionResult AddLeave(Leave leave)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Leaves.Add(leave);
                _dbContext.SaveChanges();
                return RedirectToAction("Display", "Dashboard");
            }
            return View();
        }

        public IActionResult ViewLeave()
        {
            string username = User.Identity.Name;

            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == username);
            List<Emp> employees = _dbContext.Employees.ToList();
            List<string> firstNames = employees.Select(e => e.FirstName).ToList();
            ViewBag.Employees = new SelectList(firstNames);

            return View(employee);
        }

        public IActionResult GetLeaveDetails(string name)
        {
            var employees = _dbContext.Leaves.Where(e => e.EmployeeName == name).ToList();

            return Json(employees);
        }
    }
}
