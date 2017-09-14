using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OmegaProject.Models
{
    public partial class Employee
    {
        public int EmpId { get; set; }
        [Required]
        public string EmpNo { get; set; }
        [Required]
        public string EmpName { get; set; }
        public bool EmpGender { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public string Email { get; set; }
        public bool EmpDisabled { get; set; }
        public int? DepId { get; set; }
        public int? StatusId { get; set; }
        public int? DutyId { get; set; }
        public int? PhotoId { get; set; }

        public virtual Department Dep { get; set; }
        public virtual Duty Duty { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual Status Status { get; set; }
    }
}
