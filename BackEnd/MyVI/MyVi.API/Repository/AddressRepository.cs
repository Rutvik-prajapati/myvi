using MyVi.API.Entities;
using MyVi.API.IRepository;
using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Repository
{
    public class AddressRepository : GenericRepository<AddressModel>, IAddress
    {
        public AddressRepository(MyVIDBContext context) : base(context)
        {

        }

        public bool CheckAddressIdExist(int addressId)
        {
            var isExist = false;
            isExist = context.Address.Any(x => x.Id == addressId);
            return isExist;
        }

        public override void Create(AddressModel entity)
        {
            context.Address.Add(new Address()
            {
                City = entity.City,
                State = entity.State,
                PincodeNo = entity.PincodeNo,
                FlatNo = entity.FlatNo,
                Country = entity.Country,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            });
            context.SaveChanges();
        }

        public AddressModel GetAddressById(int id)
        {
            var addressDetail = new AddressModel();

            addressDetail = context.Address.Where(x => x.IsDeleted == false)
                .Select(x => new AddressModel
                {
                    City = x.City,
                    FlatNo = x.FlatNo,
                    PincodeNo = x.PincodeNo,
                    State = x.State,
                    Country = x.Country
                })
                .FirstOrDefault();

            return addressDetail;
        }
    }
}
