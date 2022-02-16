using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyVi.API.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? VipnumberId { get; set; }
        public int? PortNumberId { get; set; }
        public int? PlanId { get; set; }
        public int? AddessId { get; set; }
        public string AlternateContactNo { get; set; }
        public int SimtypeId { get; set; }
        public int? Quantity { get; set; }
        public int? TotalPrice { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime OnUpdated { get; set; }
        public DateTime OnCreated { get; set; }

        public virtual Address Addess { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual PortNumber PortNumber { get; set; }
        public virtual Simtype Simtype { get; set; }
        public virtual User User { get; set; }
        public virtual Vipnumber Vipnumber { get; set; }
    }
}
