using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OmegaProject.Models
{
    public partial class Category
    {
        public Category()
        {
            Post = new HashSet<Post>();
        }

        public int CateId { get; set; }
        [Required]
        public string CateName { get; set; }
        public string CateNotes { get; set; }
        public bool CateDisabled { get; set; }
        public int? StatusId { get; set; }

        public virtual ICollection<Post> Post { get; set; }
        public virtual Status Status { get; set; }
    }
}
