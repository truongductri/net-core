using System;
using System.Collections.Generic;

namespace RoomApp
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Sex { get; set; }
        public DateTime  BirthDay { get; set; }
        public string Address { get; set; }
        public int? Identify { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public string MetaTitle { get; set; }
    }
}
