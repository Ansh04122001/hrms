using Hrms.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hrms.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ManagerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult ViewEmployee()
        {
            string username = User.Identity.Name;
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == username);

            if (employee != null)
            {
                ViewBag.ManagerName = employee.ManagerName;
            }

            List<string> firstNames = _dbContext.Employees
                .Where(e => e.Role == "user" && e.ManagerName == employee.ManagerName)
                .Select(e => e.FirstName)
                .ToList();

            ViewBag.Employees = new SelectList(firstNames);

            ViewBag.States = _dbContext.States.ToList();
            ViewBag.Cities = _dbContext.Cities.ToList();
            ViewBag.ExperienceLevels = _dbContext.ExperienceLevels.ToList();
           
            return View(employee);
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
                leaveViewModel.EmployeeName = employee.FirstName;
                leaveViewModel.ManagerName = employee.ManagerName;
                leaveViewModel.Department = employee.DepartmentName;
            }

            return View(leaveViewModel);
        }
        public IActionResult ManagerProfile()
        {
            string username = User.Identity.Name;
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == username);

            if (employee != null)
            {
                ViewBag.ManagerName = employee.ManagerName;
            }

            return View(employee);
        }

        public IActionResult ViewLeave()
        {
            string username = User.Identity.Name;
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == username);

            if (employee != null)
            {
                ViewBag.ManagerName = employee.ManagerName;
            }

            List<string> firstNames = _dbContext.Employees
                .Where(e => e.Role == "user" && e.ManagerName == employee.ManagerName)
                .Select(e => e.FirstName)
                .ToList();

            ViewBag.Employees = new SelectList(firstNames);

            return View(employee);
        }


        [HttpGet]
        public IActionResult Leavedetails(string employeeName, string managerName)
        {
            var employees = _dbContext.Leaves
                                      .Where(e => e.EmployeeName == employeeName && e.ManagerName == managerName)
                                      .ToList();
            if (employees.Count == 0)
            {
                return NotFound();
            }

            return Json(employees);
        }

        public IActionResult ViewTimesheet()
        {
            string username = User.Identity.Name;
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == username);

            if (employee != null)
            {
                ViewBag.ManagerName = employee.ManagerName;
            }

            List<string> firstNames = _dbContext.Employees
                .Where(e => e.Role == "user" && e.ManagerName == employee.ManagerName)
                .Select(e => e.FirstName)
                .ToList();

            ViewBag.Employees = new SelectList(firstNames);

            return View(employee);
        }

        [HttpGet]
        public IActionResult Timesheetdetails(string employeeName, string managerName)
        {
            var employees = _dbContext.Timesheets
                                      .Where(e => e.EmployeeName == employeeName && e.ManagerName == managerName)
                                      .ToList();
            if (employees.Count == 0)
            {
                return NotFound();
            }

            return Json(employees);
        }


        public IActionResult ViewAttendance()
        {

            string username = User.Identity.Name;
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == username);

            if (employee != null)
            {
                ViewBag.ManagerName = employee.ManagerName;
            }

            List<string> firstNames = _dbContext.Employees
                .Where(e => e.Role == "user" && e.ManagerName == employee.ManagerName)
                .Select(e => e.FirstName)
                .ToList();

            ViewBag.Employees = new SelectList(firstNames);

            return View(employee);
           
        }


        [HttpGet]
        public IActionResult ViewAttendanceDetails(string employeeName)
        {
            var employee = _dbContext.Employees
                                    .FirstOrDefault(e => e.FirstName == employeeName);

            if (employee == null)
            {
                return NotFound();
            }

            var query = from loginLog in _dbContext.UserLoginLogs
                        where loginLog.UserId == employee.EmailOfficial
                        select new
                        {
                            EmployeeName = employee.FirstName,
                            ManagerName = employee.ManagerName,
                            LoginTime = loginLog.LoginTime,
                            LogoutTime = loginLog.LogoutTime,
                            Date = loginLog.Date,
                            TotalLoginTime = loginLog.TotalTimeLoggedIn,
                        };

            var result = query.ToList();

            return Json(result);
        }

        public IActionResult Thumb(int id, string para)
        {
            var item = _dbContext.Leaves.Find(id);
            if (item != null)
            {
                if (para == "Accept")
                {
                    item.IsActive = true;
                }
                else if (para == "Reject")
                {
                    item.IsActive = false;
                }
                _dbContext.SaveChanges();
            }

            return Json(new { success = true });
        }


        [HttpPost]
        public IActionResult AddLeave(Leave leave)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Leaves.Add(leave);
                _dbContext.SaveChanges();
                return RedirectToAction("Dashboard", "Manager");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Employeedetails(string employeeName, string managerName)
        {
            var employees = _dbContext.Employees
                                      .Where(e => e.FirstName == employeeName && e.ManagerName == managerName)
                                      .ToList();
            if (employees.Count == 0)
            {
                return NotFound();
            }

            return Json(employees);
        }
        [HttpGet]

        public IActionResult GetEmployeeDetails(int id)
        {
            var Emp = _dbContext.Employees.FirstOrDefault(e => e.Id == id);

            if (Emp == null)
            {
                return NotFound();
            }

            return Json(Emp);
        }
    }

}
