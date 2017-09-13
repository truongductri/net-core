using System;
using System.Collections.Generic;

namespace Apartment.App.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public int? Sex { get; set; }
        public string Address { get; set; }
        public int? Identify { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public string MetaTitle { get; set; }
    }
}
