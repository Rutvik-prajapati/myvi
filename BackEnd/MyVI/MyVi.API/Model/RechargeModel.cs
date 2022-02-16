using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Model
{
    public class RechargeModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public decimal Price { get; set; }
        public string RzpOrderId { get; set; }
        public string RzpPaymentId { get; set; }
        public string RzpSignature { get; set; }
    }
}
