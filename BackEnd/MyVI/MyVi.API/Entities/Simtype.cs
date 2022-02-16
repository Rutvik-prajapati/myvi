using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyVi.API.Entities
{
    public partial class Simtype
    {
        public Simtype()
        {
            Order = new HashSet<Order>();
            PlanType = new HashSet<PlanType>();
            PortNumber = new HashSet<PortNumber>();
            Vipnumber = new HashSet<Vipnumber>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime OnUpdated { get; set; }
        public DateTime OnCreated { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<PlanType> PlanType { get; set; }
        public virtual ICollection<PortNumber> PortNumber { get; set; }
        public virtual ICollection<Vipnumber> Vipnumber { get; set; }
    }
}
