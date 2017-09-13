using System;
using System.Collections.Generic;

namespace BuildingManage
{
    public partial class Contract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public int? NumOfMonth { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string MetaTitle { get; set; }
        public bool? Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
    }
}
