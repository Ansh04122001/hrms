using Hrms.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Hrms.Controllers
{
    [Authorize(Roles = "admin,hr")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(AppDbContext dbContext , IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Display()
        {
           
            ViewBag.Department = _dbContext.Departments.ToList();
            ViewBag.Employee = _dbContext.Employees.ToList();
            ViewBag.EmployeeCount = _dbContext.Timesheets.ToList();
            ViewBag.EmployeeInActive = _dbContext.UserLoginLogs.ToList();

            return View();
        }


        public IActionResult Department()
        {
            return View();
        }
        public IActionResult DepartmentList()
        {
            return View();
        }
        public IActionResult Employee()
        {
            ViewBag.States = _dbContext.States.ToList();
            ViewBag.Cities = _dbContext.Cities.ToList();
            ViewBag.ExperienceLevels = _dbContext.ExperienceLevels.ToList();
            var departments = _dbContext.Departments.ToList();
            return View(departments);
        }
        public IActionResult Employeelist()
        {
            ViewBag.States = _dbContext.States.ToList();
            ViewBag.Cities = _dbContext.Cities.ToList();
            ViewBag.ExperienceLevels = _dbContext.ExperienceLevels.ToList();
            var departments = _dbContext.Departments.ToList();
            return View(departments);
        }

        //public IActionResult Timesheet()
        //{
        //    List<Emp> employees = _dbContext.Employees.ToList();
        //    List<string> firstNames = employees.Select(e => e.FirstName).ToList();
        //    ViewBag.Employees = new SelectList(firstNames);

        //    return View(); 
        //}

        public IActionResult Timesheet()
        {
           
            List<string> firstNames = _dbContext.Employees
                                                .Where(e => e.Role == "user")
                                                .Select(e => e.FirstName)
                                                .ToList();
        
            ViewBag.Employees = new SelectList(firstNames);

            return View();
        }


        public IActionResult ViewLeave()
        {

            List<string> firstNames = _dbContext.Employees
                                                .Select(e => e.FirstName)
                                                .ToList();

            ViewBag.Employees = new SelectList(firstNames);

            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Dept(Department department)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Departments.Add(department);
                _dbContext.SaveChanges();
                return RedirectToAction("Display", "Admin"); 
            }
            return View(); 
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departments = _dbContext.Departments.Where(e => e.IsDeleted == false).ToList();
           
            return Json(departments);
        }
        [HttpGet]
        public IActionResult GetDepartmentDetails(int id)
        {
            var department = _dbContext.Departments.FirstOrDefault(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            return Json(department);
        }
        [HttpPost]
        public IActionResult Update(Department department)
        {
            if (ModelState.IsValid)
            {
                var existingDepartment = _dbContext.Departments.Find(department.Id);
                if (existingDepartment == null)
                {
                    return NotFound();
                }
                existingDepartment.Name = department.Name;
                existingDepartment.Code = department.Code;
                _dbContext.SaveChanges();
                return RedirectToAction("Display", "Admin");
            }
            return View(department);
        }

        private string SaveImage(IFormFile file, string folderName)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return "/" + folderName + "/" + uniqueFileName;
        }
        [HttpGet]
        public IActionResult GetEmpList()
        {
            var employees = _dbContext.Employees.Where(e => e.IsDeleted == false).ToList();
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

        [HttpPost]
        public IActionResult EmpUpdate([FromForm] Emp employee)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee = _dbContext.Employees.Find(employee.Id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }
                existingEmployee.EmployeeCode = employee.EmployeeCode;
                existingEmployee.DepartmentName = employee.DepartmentName;
                existingEmployee.ManagerName = employee.ManagerName;
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.DateOfBirth = employee.DateOfBirth;
                existingEmployee.Designation = employee.Designation;
                existingEmployee.DateOfJoining = employee.DateOfJoining;
                existingEmployee.ExperienceLevel = employee.ExperienceLevel;
                existingEmployee.Experience = employee.Experience;
                existingEmployee.FatherName = employee.FatherName;
                existingEmployee.MaritalStatus = employee.MaritalStatus;
                existingEmployee.AnniversaryDate = employee.AnniversaryDate;
                existingEmployee.EmployeeContact = employee.EmployeeContact;
                existingEmployee.AlternateNumber = employee.AlternateNumber;
                existingEmployee.EmailOfficial = employee.EmailOfficial;
                existingEmployee.EmailPersonal = employee.EmailPersonal;
                existingEmployee.AadharCardNumber = employee.AadharCardNumber;
                existingEmployee.PANCardNumber = employee.PANCardNumber;
                existingEmployee.PresentAddress = employee.PresentAddress;
                existingEmployee.PermanentAddress = employee.PermanentAddress;
                existingEmployee.Password = employee.Password;
                existingEmployee.StateId = employee.StateId;
                existingEmployee.CityId = employee.CityId;
                if (employee.PANCardFile != null)
                {
                    existingEmployee.PANCard = SaveImage(employee.PANCardFile, "PANCard");
                }
                if (employee.EmpImagesFile != null)
                {
                    existingEmployee.EmpImages = SaveImage(employee.EmpImagesFile, "EmpImages");
                }
                if (employee.AadhaarCardFile != null)
                {
                    existingEmployee.AadhaarCard = SaveImage(employee.AadhaarCardFile, "AadhaarCard");
                }
                if (employee.BankDetailsFile != null)
                {
                    existingEmployee.BankDetails = SaveImage(employee.BankDetailsFile, "BankDetails");
                }
                if (employee.EducationDetailsFile != null)
                {
                    existingEmployee.EducationDetails = SaveImage(employee.EducationDetailsFile, "EducationDetails");
                }
                _dbContext.SaveChanges();
                return RedirectToAction("Display", "Admin");
            }
            return View(employee);
        }
        public IActionResult ToggleThumb(int id)
        {
            var item = _dbContext.Employees.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _dbContext.SaveChanges();
            }

            return Json(new { success = true });
        }
        public IActionResult IsDelete(int id, string action)
        {
            var item = _dbContext.Employees.Find(id);
            if (item != null && action == "IsDelete")
            {
                item.IsDeleted = true;
                _dbContext.SaveChanges();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult CheckContactNumber(string contactNumber)
        {
            bool exists = _dbContext.Employees.Any(e => e.EmployeeContact == contactNumber && e.IsDeleted == false);
            return Json(new { exists = exists });
        }

        [HttpPost]
        public IActionResult CheckEmail(string email)
        {
            bool exists = _dbContext.Employees.Any(e => e.EmailPersonal == email && e.IsDeleted == false);
            return Json(new { exists = exists });
        }
        [HttpPost]
        public IActionResult CheckAadharCardNumber(string aadharCardNumber)
        {
            bool exists = _dbContext.Employees.Any(e => e.AadharCardNumber == aadharCardNumber && e.IsDeleted == false);
            return Json(new { exists = exists });
        }
        [HttpPost]
        public IActionResult CheckPANCardNumber(string panCardNumber )
        {
            bool exists = _dbContext.Employees.Any(e => e.PANCardNumber == panCardNumber && e.IsDeleted == false);
            return Json(new { exists = exists });
        }

        public IActionResult Delete(int id, string action)
        {
            var item = _dbContext.Departments.Find(id);
            if (item != null && action == "Delete")
            {
                item.IsDeleted = true;
                _dbContext.SaveChanges();
            }

            return Json(new { success = true });
        }
        public IActionResult Thumb(int id, string para)
        {
            var item = _dbContext.Employees.Find(id);
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

        [HttpGet]
        public IActionResult Timesheetdetails(string employeeName)
        {
            var employees = _dbContext.Timesheets.Where(e => e.EmployeeName == employeeName).ToList();
            if (employees.Count == 0)
            {
                return NotFound();
            }

            return Json(employees);
        }
        public IActionResult AttendanceManage()
        {
            // Fetch employees with the role "user"
            List<string?> officialEmails = _dbContext.Employees
                .Where(e => e.Role == "user")
                .Select(e => e.EmailOfficial)
                .ToList();

            // Pass the list of emails to the ViewBag as a SelectList
            ViewBag.Employees = new SelectList(officialEmails);

            return View();
        }




        [HttpGet]
        public IActionResult AttendanceDetails(string employeeName)
        {
            try
            {
                var employeeDetails = _dbContext.UserLoginLogs.Where(e => e.UserId == employeeName).ToList();
                return Json(employeeDetails);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ViewLeavedetails(string employeeName)
        {
            var employees = _dbContext.Leaves.Where(e => e.EmployeeName == employeeName).ToList();
            if (employees.Count == 0)
            {
                return NotFound();
            }

            return Json(employees);
        }
    }
}
