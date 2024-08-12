using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hrms.Models.DTO
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department Code is required")]
        public string Code { get; set; }

        public bool? IsDeleted { get; set; } = false;
        public bool? IsActive { get; set; } = false;


    }
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
    }
    public class ExperienceLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class Emp
    {
        public int Id { get; set; }  

        public string EmployeeCode { get; set; }

        public string DepartmentName { get; set; }  
        public string ManagerName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Designation { get; set; }

        public DateTime DateOfJoining { get; set; }

        public string ExperienceLevel { get; set; }

        public string? Experience { get; set; }

        public string FatherName { get; set; }

        public string MaritalStatus { get; set; }

        public DateTime? AnniversaryDate { get; set; }

        public string EmployeeContact { get; set; }

        public string AlternateNumber { get; set; }

        public string? EmailOfficial { get; set; }

        public string EmailPersonal { get; set; }

        public string AadharCardNumber { get; set; }

        public string PANCardNumber { get; set; }

        public string PresentAddress { get; set; }

        public string PermanentAddress { get; set; }
        public string? Password { get; set; }

        public string? Role { get; set; }
        public int StateId { get; set; } 

        public int CityId { get; set; }
        public bool? IsActive { get; set; } = false;

        public bool? IsDeleted{ get; set; } = false;
        public string? PANCard { get; set; }

        public string? EmpImages { get; set; }

        public string? AadhaarCard { get; set; }

        public string? BankDetails { get; set; }

        public string? EducationDetails { get; set; }



        [NotMapped]
        public IFormFile? PANCardFile { get; set; }

        [NotMapped]
        public IFormFile? EmpImagesFile { get; set; }

        [NotMapped]
        public IFormFile? AadhaarCardFile { get; set; }

        [NotMapped]
        public IFormFile? BankDetailsFile { get; set; }

        [NotMapped]
        public IFormFile? EducationDetailsFile { get; set; }




    }

    public class Timesheet
    {

        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string ManagerName { get; set; }
        public string Department { get; set; }
        public string ProjectName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class UserLoginLogs
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public TimeSpan? TotalTimeLoggedIn { get; set; }
    }
    public class Leave
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string ManagerName { get; set; }
        public string Department { get; set; }
        [Required]
        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        [CustomDateValidation(ErrorMessage = "From Date cannot be before the current date.")]
        public DateTime FromDate { get; set; }

        [Required]
        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        [CustomDateValidation(ErrorMessage = "To Date cannot be before the current date.")]
        public DateTime ToDate { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; } = false;
    }
    public class CustomDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateValue = (DateTime)value;
            if (dateValue < DateTime.Now.Date)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
