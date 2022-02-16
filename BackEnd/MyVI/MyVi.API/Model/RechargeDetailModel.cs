using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Model
{
    public class RechargeDetailModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ContactNo { get; set; }
        public int PlanId { get; set; }

        public int PlanTypeId { get; set; }
        public int? Price { get; set; }
        public string Call { get; set; }
        public string Data { get; set; }
        public string Sms { get; set; }
        public int? Validity { get; set; }
        public string Benefits { get; set; }

        public string CardNumber { get; set; }
        public string Expiry { get; set; }
        public int Cvv { get; set; }
    }
}
