using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OmegaProject.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        [Required]
        public string EventName { get; set; }
        public string EventInfo1 { get; set; }
        public string EventInfo2 { get; set; }
        public string EventInfo3 { get; set; }
        public string EventInfo4 { get; set; }
        public string EventInfo5 { get; set; }
        public int? PhotoId { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
