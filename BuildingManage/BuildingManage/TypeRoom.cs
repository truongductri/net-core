using System;
using System.Collections.Generic;

namespace BuildingManage
{
    public partial class TypeRoom
    {
        public TypeRoom()
        {
            Room = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Room> Room { get; set; }
    }
}
