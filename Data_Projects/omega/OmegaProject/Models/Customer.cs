using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OmegaProject.Models
{
    public partial class Customer
    {
        public int CusId { get; set; }
        [Required]
        public string CusNo { get; set; }
        public string CusName { get; set; }
        public DateTime CusDateCreate { get; set; }
        public DateTime CusLastUpdate { get; set; }
        public int? BraId { get; set; }
        public int? StatusId { get; set; }
        public int? PhotoId { get; set; }
        public string CusNotes { get; set; }
        public string CusAddress { get; set; }
        public string CusWeb { get; set; }

        public virtual Branch Bra { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual Status Status { get; set; }
    }
}
