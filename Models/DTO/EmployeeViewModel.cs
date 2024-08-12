namespace Hrms.Models.DTO
{
    public class EmployeeViewModel
    {
       
        public List<State> States { get; set; }
        public List<City> Cities { get; set; }
        public List<Department> Departments { get; set; }

        public List<ExperienceLevel> ExperienceLevels { get; set; }
    }

}
