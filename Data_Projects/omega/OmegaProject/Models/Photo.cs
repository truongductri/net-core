using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OmegaProject.Models
{
    public partial class Photo
    {
        public Photo()
        {
            Customer = new HashSet<Customer>();
            Employee = new HashSet<Employee>();
            Event = new HashSet<Event>();
            Post = new HashSet<Post>();
            Product = new HashSet<Product>();
        }

        public int PhotoId { get; set; }
        [Required]
        public string PhotoName { get; set; }
        public string PhotoThumbNail { get; set; }
        public string PhotoLarge01 { get; set; }
        public string PhotoLarge02 { get; set; }
        public string PhotoLarge03 { get; set; }
        public string PhotoLarge04 { get; set; }
        public string PhotoLarge05 { get; set; }
        public string PhotoNotes { get; set; }
        public bool PhotoDisabled { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
