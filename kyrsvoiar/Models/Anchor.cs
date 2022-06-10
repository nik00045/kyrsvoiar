using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace kyrsvoiar.Models
{
    public partial class Anchor
    {
        public int Idanchor { get; set; }
        public int Idiot { get; set; }
        public string Code { get; set; }

        public virtual Iot IdiotNavigation { get; set; }
    }
}
