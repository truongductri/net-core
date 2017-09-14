using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OmegaProject.Models
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }

        public int DepId { get; set; }
        [Required]
        public string DepNo { get; set; }
        [Required]
        public string DepName { get; set; }
        public string DepNotes { get; set; }
        public bool DepDisabled { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
