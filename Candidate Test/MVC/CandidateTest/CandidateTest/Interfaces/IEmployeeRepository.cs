using CandidateTest.Models;

namespace CandidateTest.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Question1Model>> GetAllEmployeesFirstAsync();
        Task<IEnumerable<Question2Model>> GetAllEmployeesSecondAsync();
        Task<IEnumerable<Question3Model>> GetAllEmployeesThridAsync();
        Task<IEnumerable<Question4Model>> GetAllEmployeesFourthAsync();
        Task<IEnumerable<Question5Model>> GetAllEmployeesFirthAsync();
        Task<Department> GetDepartmentAsync(string departmentName);
        Task<bool> AddEmployee(Employee employee);
    }
}
