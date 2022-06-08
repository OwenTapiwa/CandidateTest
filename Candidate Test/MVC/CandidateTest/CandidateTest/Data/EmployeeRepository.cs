using CandidateTest.Interfaces;
using CandidateTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateTest.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CandidateTestContext _context;


        public EmployeeRepository(CandidateTestContext context)
        {
            _context = context;
        }

        public async Task<bool> AddEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Added;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Question1Model>> GetAllEmployeesFirstAsync()
        {
            var result = (from emp in _context.Employees
                          join dep in _context.Departments on emp.DepartmentId equals dep.Id
                          select new Question1Model
                          {
                              FirstName = emp.FirstName,
                              LastName = emp.LastName,
                              DepartmentName = dep.Name
                             
                          }).Distinct();
            return result.ToList();
        }

        public async Task<IEnumerable<Question5Model>> GetAllEmployeesFirthAsync()
        {
            var result = (from emp in _context.Employees
                          join dep in _context.Departments on emp.DepartmentId equals dep.Id
                          select new Question5Model
                          {
                              FirstName = emp.FirstName,
                              LastName = emp.LastName,
                              isMillennial = ((DateTime.Parse(emp.DateOfBirth.ToString()).Year >= DateTime.Parse("01/01/1981").Year) && (DateTime.Parse(emp.DateOfBirth.ToString()).Year <= DateTime.Parse("01/01/1996").Year)) ? "yes" : "no"
                           }).Distinct();
            return result.ToList();
        }

        public async Task<IEnumerable<Question4Model>> GetAllEmployeesFourthAsync()
        {
            var result = (from emp in this._context.Employees
                         join dept in this._context.Departments on emp.DepartmentId equals dept.Id
                         group emp by dept.Name into grp
                         select new Question4Model
                         {
                             DepartmentName = grp.Key,
                             MaxSalary = Convert.ToDouble(grp.Max(ed => ed.Salary))
                         }).Distinct(); 

            return result.ToList();
        }

        public async Task<IEnumerable<Question2Model>> GetAllEmployeesSecondAsync()
        {

            var result = (from emp in this._context.Employees
                          join dept in this._context.Departments on emp.DepartmentId equals dept.Id
                          group emp by dept.Name into grp
                          orderby grp.Average(ed => ed.Salary)
                          select new Question2Model
                          {
                              DepartmentName = grp.Key,
                              NumberOfEmployees = grp.Count(),
                              AverageSalary = Convert.ToDouble(grp.Average(ed => ed.Salary))
                          }).Distinct();
            
            return result.ToList();
        }

        public async Task<IEnumerable<Question3Model>> GetAllEmployeesThridAsync()
        {
            var result = (from emp in this._context.Employees
                         join dept in this._context.Departments on emp.DepartmentId equals dept.Id
                         orderby emp.Salary
                         select new Question3Model
                         {
                             FirstName = emp.FirstName,
                             LastName= emp.LastName,
                             Salary = Convert.ToDouble(emp.Salary),
                             SalaryIncrease = Convert.ToDouble(emp.Salary) + (Convert.ToDouble(emp.Salary) * 0.1),
                             DepartmentName = dept.Name,
                         }).Distinct();

            return result.ToList();
        }

        public async Task<Department> GetDepartmentAsync(string departmentName)
        {
            return  _context.Departments.SingleOrDefault(name => name.Name == departmentName);
            
        }
    }
}
