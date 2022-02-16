using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyVi.API.Entities
{
    public partial class RoamingPlan
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int CountryId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime OnUpdated { get; set; }
        public DateTime OnCreated { get; set; }

        public virtual Country Country { get; set; }
        public virtual Plan Plan { get; set; }
    }
}
