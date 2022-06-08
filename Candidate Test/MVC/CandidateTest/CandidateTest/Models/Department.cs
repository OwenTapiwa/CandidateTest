using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CandidateTest.Models
{
    public partial class Department
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
