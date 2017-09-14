using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OmegaProject.Models
{
    public partial class Status
    {
        public Status()
        {
            Category = new HashSet<Category>();
            Customer = new HashSet<Customer>();
            Employee = new HashSet<Employee>();
            Post = new HashSet<Post>();
        }

        public int StatusId { get; set; }
        [Required]
        public string StatusName { get; set; }
        public string StatusNotes { get; set; }
        public bool StatusDisabled { get; set; }
        public string TableName { get; set; }

        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
