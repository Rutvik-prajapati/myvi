using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Model
{
    public class AddressModel
    {
        //public int UserId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int FlatNo { get; set; }
        public string Country { get; set; }
        public string PincodeNo { get; set; }
    }
}
