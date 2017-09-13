using System;
using System.Collections.Generic;

namespace BuildingManage
{
    public partial class Room
    {
        public Room()
        {
            Contract = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int? Capacity { get; set; }
        public int? Floor { get; set; }
        public long? Prices { get; set; }
        public string Image { get; set; }
        public string MetaTiTle { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Contract> Contract { get; set; }
        public virtual TypeRoom TypeNavigation { get; set; }
    }
}
