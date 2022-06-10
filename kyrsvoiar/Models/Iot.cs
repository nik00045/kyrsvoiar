using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace kyrsvoiar.Models
{
    public partial class Iot
    {
        public Iot()
        {
            Anchor = new HashSet<Anchor>();
        }

        public int Idiot { get; set; }
        public int Idbuilding { get; set; }
        public string Price { get; set; }
        public string Coordinatex { get; set; }
        public string Coordinatey { get; set; }

        public virtual Building IdbuildingNavigation { get; set; }
        public virtual ICollection<Anchor> Anchor { get; set; }
    }
}
