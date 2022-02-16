using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyVi.API.Entities
{
    public partial class PortNumber
    {
        public PortNumber()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public int SimtypeId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime OnUpdated { get; set; }
        public DateTime OnCreated { get; set; }

        public virtual Simtype Simtype { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
