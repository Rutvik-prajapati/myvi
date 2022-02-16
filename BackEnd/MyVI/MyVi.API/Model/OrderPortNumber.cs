using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Model
{
    public class OrderPortNumber
    {
        public int Id { get; set; }//if not require comment it
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public string AlternateContactNo { get; set; }
        public int SimtypeId { get; set; }
        public int Status { get; set; }

        public string PortNumber { get; set; }
        //public int VIPNumberId { get; set; }//if not require comment it

        public string City { get; set; }
        public string State { get; set; }
        public int FlatNo { get; set; }
        public string Country { get; set; }
        public string PincodeNo { get; set; }
    }
}
