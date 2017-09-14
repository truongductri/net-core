using System;
using System.Collections.Generic;

namespace OmegaProject.Models
{
    public partial class ContactUs
    {
        public int ConId { get; set; }
        public string ConTitle { get; set; }
        public string ConName { get; set; }
        public string ConEmail { get; set; }
        public string ConPhone { get; set; }
        public string ConContent { get; set; }
    }
}
