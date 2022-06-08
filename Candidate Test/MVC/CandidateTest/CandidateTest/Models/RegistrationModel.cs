using System.ComponentModel.DataAnnotations;

namespace CandidateTest.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Please Enter FirstName..")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter LastName..")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter DateOfBirth..")]
        [Display(Name = "DateOfBirth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Select Department..")]
        [Display(Name = "DepartmentName")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Please Enter Salary..")]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }
    }
}
