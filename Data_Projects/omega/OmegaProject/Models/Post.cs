using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OmegaProject.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostContent { get; set; }
        public string PostDescription { get; set; }
        public string PostLabel { get; set; }
        public bool PostDisabled { get; set; }
        public DateTime? PostDateCreate { get; set; }
        public DateTime? PostLastUpdate { get; set; }
        public int? StatusId { get; set; }
        public int? CategoryId { get; set; }
        public int? PhotoId { get; set; }
        public string PostUserCreate { get; set; }
        public string PostUserUpdate { get; set; }

        public virtual Category Category { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual AspNetUsers PostUserCreateNavigation { get; set; }
        public virtual AspNetUsers PostUserUpdateNavigation { get; set; }
        public virtual Status Status { get; set; }
    }
}
