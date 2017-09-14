using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OmegaProject.Models
{
    public partial class Duty
    {
        public Duty()
        {
            Employee = new HashSet<Employee>();
        }

        public int DutyId { get; set; }
        [Required]
        public string DutyNo { get; set; }
        [Required]
        public string DutyName { get; set; }
        public string DutyNotes { get; set; }
        public bool DutyDisabled { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
