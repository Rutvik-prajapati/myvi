using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.IRepository
{
    public interface IAddress : IGenericInterface<AddressModel>
    {
        public bool CheckAddressIdExist(int addressId);
        public AddressModel GetAddressById(int id);
    }
}
