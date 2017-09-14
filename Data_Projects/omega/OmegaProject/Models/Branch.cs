using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OmegaProject.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Customer = new HashSet<Customer>();
        }

        public int BraId { get; set; }
        [Required]
        public string BraNo { get; set; }
        [Required]
        public string BraName { get; set; }
        public string BraNotes { get; set; }
        public bool BraDisabled { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
    }
}
