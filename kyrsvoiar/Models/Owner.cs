using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace kyrsvoiar.Models
{
    public partial class Owner
    {
        public Owner()
        {
            Building = new HashSet<Building>();
        }

        public int Idowner { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Building> Building { get; set; }
    }
}
