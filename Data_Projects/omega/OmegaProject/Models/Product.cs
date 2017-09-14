using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OmegaProject.Models
{
    public partial class Product
    {
        public int ProdId { get; set; }
        [Required]
        public string ProdNo { get; set; }
        [Required]
        public string ProdName { get; set; }
        public string ProdDescription { get; set; }
        public string ProdDescription1 { get; set; }
        public string ProdDescription2 { get; set; }
        public string ProdDescription3 { get; set; }
        public string ProdDescription4 { get; set; }
        public string ProdNotes { get; set; }
        public bool ProdDisabled { get; set; }
        public int? PhotoId { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
