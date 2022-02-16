using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Model
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string AlternateContactNo { get; set; }
        public int SIMtypeId { get; set; }
        public string SIMtypeName { get; set; }
        public int? PortNumberId { get; set; }
        public string PortNumber { get; set; }
        public int? VIPNumberId { get; set; }
        public string VIPNumber { get; set; }
        public int? Quantity { get; set; }
        public int? TotalPrice { get; set; }
        public string Status { get; set; }
        public int? AddessId { get; set; }
    }
}
