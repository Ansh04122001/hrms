using Hrms.Models.Domain;
using Hrms.Models.DTO;
using Hrms.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;



namespace Hrms.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserAuthenticationService(
      UserManager<ApplicationUser> userManager,
      SignInManager<ApplicationUser> signInManager,
      RoleManager<IdentityRole> roleManager,
      AppDbContext dbContext,
      IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this._dbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;

        }

        public async Task<Status> RegisterAsync(RegistrationModel model)
        {
            var status = new Status();
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exist";
                return status;
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));


            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            status.StatusCode = 1;
            status.Message = "You have registered successfully";
            return status;
        }
        public async Task<Status> EmpAsync(Emp employee, ModelStateDictionary modelState)
        {
            var status = new Status();
            var userExists = await userManager.FindByNameAsync(employee.EmployeeContact);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exists";
                return status;
            }

            // Check if the ModelState is valid before proceeding
            if (!modelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Invalid model state";
                return status;
            }

            // Generate a password
            string password = "Admin@123";

            // Save the password in the database
            if (string.IsNullOrEmpty(employee.Password))
            {
                employee.Password = password;
            }

            // Save images
            if (employee.PANCardFile != null)
            {
                employee.PANCard = SaveImage(employee.PANCardFile, "PANCard");
            }
            if (employee.EmpImagesFile != null)
            {
                employee.EmpImages = SaveImage(employee.EmpImagesFile, "EmpImages");
            }
            if (employee.AadhaarCardFile != null)
            {
                employee.AadhaarCard = SaveImage(employee.AadhaarCardFile, "AadhaarCard");
            }
            if (employee.BankDetailsFile != null)
            {
                employee.BankDetails = SaveImage(employee.BankDetailsFile, "BankDetails");
            }
            if (employee.EducationDetailsFile != null)
            {
                employee.EducationDetails = SaveImage(employee.EducationDetailsFile, "EducationDetails");
            }



            var user = new ApplicationUser()
            {
                UserName = employee.EmailOfficial,
                Email = employee.EmailPersonal,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            var result = await userManager.CreateAsync(user, employee.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();

            var role = employee.Role;
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            if (await roleManager.RoleExistsAsync(role))
            {
                await userManager.AddToRoleAsync(user, role);
            }

            status.StatusCode = 2;
            status.Message = "You have registered successfully";
            return status;
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

        public async Task<Status> LoginAsync(LoginModel model)
        {
            var status = new Status();
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid password";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
        };
                var employee = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == user.UserName);

                bool roleMatched = false;

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    if (userRole == "user" && employee.IsActive == true)
                    {
                        status.StatusCode = 2;
                        status.Message = "Logged in successfully";
                        roleMatched = true;
                        break;
                    }
                    if (userRole == "Manager")
                    {
                        status.StatusCode = 3;
                        status.Message = "Logged in successfully";
                        roleMatched = true;
                        break;
                    }
                    if (userRole == "hr")
                    {
                        status.StatusCode = 4;
                        status.Message = "Logged in successfully";
                        roleMatched = true;
                        break;
                    }
                    if (userRole == "admin")
                    {
                        status.StatusCode = 1;
                        status.Message = "Logged in successfully";
                        roleMatched = true;
                        break;
                    }
                }

                if (!roleMatched)
                {
                    status.StatusCode = 5;
                    status.Message = "Logged in Inactive";
                }
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User is locked out";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on logging in";
            }

            return status;
        }


        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();

        }

        public async Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username)
        {
            var status = new Status();

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                status.Message = "User does not exist";
                status.StatusCode = 0;
                return status;
            }
            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var emp = _dbContext.Employees.FirstOrDefault(e => e.EmailOfficial == user.UserName);
                if (emp != null)
                {
                    emp.Password = model.NewPassword;
                    await _dbContext.SaveChangesAsync();
                    status.Message = "Password has updated successfully";
                    status.StatusCode = 1;
                }
                else
                {
                    status.Message = "Password has updated successfully";
                    status.StatusCode = 1;
                }
               
            }
            else
            {
                status.Message = "Some error occcured";
                status.StatusCode = 0;
            }
            return status;

        }


    }
}
