using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CandidateTest.Models
{
    public partial class Employee
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? DepartmentId { get; set; }
        public decimal? Salary { get; set; }
    }
}
