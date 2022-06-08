using CandidateTest.Interfaces;
using CandidateTest.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Web;
using System.IO;
using System.Globalization;

namespace CandidateTest.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly CandidateTestContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(CandidateTestContext context,IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;   
        }
        // GET: EmployeeController
        public async Task<ActionResult> IndexAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }

        public async Task<ActionResult> FirstQueryAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesFirstAsync();
            return View(employees);
        }

        public async Task<ActionResult> SecondQueryAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesSecondAsync();
            return View(employees);
        }

        public async Task<ActionResult> ThirdQueryAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesThridAsync();
            return View(employees);
        }

        public async Task<ActionResult> FourthQueryAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesFourthAsync();
            return View(employees);
        }

        public async Task<ActionResult> FirthQueryAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesFirthAsync();
            return View(employees);
        }

        public ActionResult UploadAsync()
        {
            return View(new List<RegistrationModel>());
        }


        [HttpPost]
        public async Task<ActionResult> UploadAsync(IFormFile postedFile)
        {
            try
            {
                List<RegistrationModel> registrations = new List<RegistrationModel>();
                string webRootPath = _webHostEnvironment.WebRootPath;
                string filePath = string.Empty;

                if (postedFile != null)
                {
                    string path = Path.Combine(webRootPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + "\\" + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    if (extension != ".csv")
                    {
                        ViewBag.errorMessage = "Only CSVs are allowed";
                        return View(registrations);
                    }

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await postedFile.CopyToAsync(fileStream);
                    }

                    //Read the contents of CSV file.
                    DataTable dz = new DataTable();
                    using (var csv = new CsvReader(System.IO.File.OpenText(filePath), System.Globalization.CultureInfo.CurrentCulture))
                    {
                        // Do any configuration to `CsvReader` before creating CsvDataReader.
                        using (var dr = new CsvDataReader(csv))
                        {
                            //var dt = new DataTable();
                            dz.Load(dr);
                        }
                    }

                    if (dz.Rows.Count > 0)
                    {
                        for (int i = 0; i < dz.Rows.Count; i++)
                        {
                            registrations.Add(new RegistrationModel
                            {
                                FirstName = dz.Rows[i]["firstName"].ToString(),
                                LastName = dz.Rows[i]["lastName"].ToString(),
                                DateOfBirth = Convert.ToDateTime(dz.Rows[i]["dateOfBirth"].ToString()),
                                DepartmentName = dz.Rows[i]["DepartmentName"].ToString(),
                                Salary = decimal.Parse(dz.Rows[i]["Salary"].ToString(), CultureInfo.InvariantCulture),
                            });
                        }

                    }

                }
                else
                {
                    ViewBag.errorMessage = "Select File To Upload";
                    return View(registrations);
                }
                ViewBag.Message = "File Uploaded!";
                return View(registrations);
            }
            catch (Exception ex)
            {
                List<RegistrationModel> registrations = new List<RegistrationModel>();
                ViewBag.errorMessage =ex.Message;
                return View(registrations);
            }
        }

        // GET: EmployeeController/Create
        public ActionResult AddEmployee()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEmployee(RegistrationModel registration)
        {
            try
            {
                //get department id
                var dep = _employeeRepository.GetDepartmentAsync(registration.DepartmentName);
                if(dep == null)
                {
                    ViewBag.errorMessage = "Error Could not save the user";
                    return View();
                }
                var employee = new Employee
                {
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,   
                    DateOfBirth = registration.DateOfBirth,
                    DepartmentId = dep.Result.Id,
                    Salary = registration.Salary
                };
                if (await _employeeRepository.AddEmployee(employee))
                {
                    ViewBag.message = "The user is saved successfully";
                    return View();
                }
                else
                {
                    ViewBag.errorMessage = "Error Could not save the user";
                    return View();
                }
                return View();

            }
            catch(Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }
        }
        
    }
}
