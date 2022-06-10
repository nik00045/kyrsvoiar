using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace kyrsvoiar.Models
{
    public partial class Building
    {
        public Building()
        {
            Iot = new HashSet<Iot>();
        }

        public int Idbuilding { get; set; }
        public int Idowner { get; set; }
        public string Admincode { get; set; }

        public virtual Owner IdownerNavigation { get; set; }
        public virtual ICollection<Iot> Iot { get; set; }
    }
}
