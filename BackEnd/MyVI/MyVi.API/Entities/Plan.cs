using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyVi.API.Entities
{
    public partial class Plan
    {
        public Plan()
        {
            Order = new HashSet<Order>();
            RoamingPlan = new HashSet<RoamingPlan>();
            UserRechargeHistory = new HashSet<UserRechargeHistory>();
        }

        public int Id { get; set; }
        public int PlanTypeId { get; set; }
        public int? Price { get; set; }
        public double? Talktime { get; set; }
        public string Call { get; set; }
        public string Data { get; set; }
        public string Sms { get; set; }
        public int? Validity { get; set; }
        public int? Connection { get; set; }
        public string Benefits { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime OnUpdated { get; set; }
        public DateTime OnCreated { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<RoamingPlan> RoamingPlan { get; set; }
        public virtual ICollection<UserRechargeHistory> UserRechargeHistory { get; set; }
    }
}
