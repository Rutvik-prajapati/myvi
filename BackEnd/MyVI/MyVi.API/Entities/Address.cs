using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyVi.API.Entities
{
    public partial class Address
    {
        public Address()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int FlatNo { get; set; }
        public string Country { get; set; }
        public string PincodeNo { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime OnUpdated { get; set; }
        public DateTime OnCreated { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
