using Hrms.Models.Domain;
using Hrms.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hrms.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {

        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);
        Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);

        Task<Status> EmpAsync(Emp employee, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState);


    }
}
