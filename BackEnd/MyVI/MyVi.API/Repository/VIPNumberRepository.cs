using MyVi.API.Entities;
using MyVi.API.IRepository;
using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Repository
{
    public class VIPNumberRepository : GenericRepository<OrderVIPNumber>, IVIPNumber
    {
        public VIPNumberRepository(MyVIDBContext context) : base(context)
        {

        }
        public override void Create(OrderVIPNumber entity)
        {
            var newAddress = new Address()
            {
                City = entity.City,
                State = entity.State,
                FlatNo = entity.FlatNo,
                Country = entity.Country,
                PincodeNo = entity.PincodeNo,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            };
            context.Address.Add(newAddress);
            context.SaveChanges();

            var plan = context.Plan.Where(x => x.Id == entity.PlanId).FirstOrDefault();

            context.Order.Add(new Order()
            {
                UserId = entity.UserId,
                PlanId = entity.PlanId,
                AlternateContactNo = entity.AlternateContactNo,
                SimtypeId = entity.SimtypeId,
                Quantity = HelperConstant.SIMQuantity,
                TotalPrice = (HelperConstant.SIMPrice * HelperConstant.SIMQuantity) + plan.Price,
                Status = entity.Status,  //conformed=2
                AddessId = newAddress.Id,
                VipnumberId = entity.VIPNumberId,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            });
            context.SaveChanges();
        }

        public override void Delete(int id)
        {
            var registeredOrder = context.Order.Where(x => x.Id == id &&
                                                      x.PortNumberId == null)
                                               .FirstOrDefault();
            registeredOrder.IsDeleted = true;
            registeredOrder.OnUpdated = DateTime.Now;
            context.SaveChanges();
        }

        public override void Update(int id, OrderVIPNumber entity)
        {
            var registeredOrder = context.Order.Where(x => x.Id == id &&
                                                           x.PortNumberId == null).FirstOrDefault();
            registeredOrder.UserId = entity.UserId;
            registeredOrder.PlanId = entity.PlanId;
            registeredOrder.AlternateContactNo = entity.AlternateContactNo;
            registeredOrder.SimtypeId = entity.SimtypeId;
            registeredOrder.Status = entity.Status;  //conformed=2
            registeredOrder.AddessId = entity.Id;
            registeredOrder.VipnumberId = entity.VIPNumberId;
            registeredOrder.OnUpdated = DateTime.Now;
            context.SaveChanges();

            var orderAddress = context.Address.Where(x => x.Id == registeredOrder.AddessId).FirstOrDefault();
            orderAddress.City = entity.City;
            orderAddress.Country = entity.Country;
            orderAddress.FlatNo = entity.FlatNo;
            orderAddress.State = entity.State;
            orderAddress.PincodeNo = entity.PincodeNo;
            orderAddress.OnUpdated = DateTime.Now;
            context.SaveChanges();
        }
        public IEnumerable<OrderModel> GetAllOrderedVIPNumber()
        {
            var listOfVIPNumberOrder = new List<OrderModel>();

            listOfVIPNumberOrder = context.Order.Where(x => x.IsDeleted == false &&
                                                        x.VipnumberId != null)
                .Select(x => new OrderModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    AlternateContactNo = x.AlternateContactNo,
                    SIMtypeName = x.Simtype.Name,
                    SIMtypeId = x.SimtypeId,
                    VIPNumberId = x.VipnumberId,
                    VIPNumber = x.Vipnumber.Number,
                    Quantity = x.Quantity,
                    TotalPrice = x.TotalPrice,
                    AddessId = x.AddessId,
                    Status = ((OrderStatus)x.Status).ToString()
                }).ToList();

            return listOfVIPNumberOrder;
        }

        public OrderModel GetOrderedVIPNumberById(int id)
        {
            var vipNumberOrder = new OrderModel();

            vipNumberOrder = context.Order.Where(x => x.IsDeleted == false &&
                                                        x.VipnumberId != null)
                .Select(x => new OrderModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    AlternateContactNo = x.AlternateContactNo,
                    SIMtypeName = x.Simtype.Name,
                    SIMtypeId = x.SimtypeId,
                    VIPNumberId = x.VipnumberId,
                    VIPNumber = x.Vipnumber.Number,
                    Quantity = x.Quantity,
                    TotalPrice = x.TotalPrice,
                    AddessId = x.AddessId,
                    Status = ((OrderStatus)x.Status).ToString()
                }).FirstOrDefault();

            return vipNumberOrder;
        }

        public bool CheckVIPNumberIdExist(int id)
        {
            var isExist = false;
            isExist = context.Vipnumber.Any(x => x.Id == id);
            return isExist;
        }

        public IEnumerable<VIPNumberModel> GetAllVIPNumbers()
        {
            var vipNumberList = new List<VIPNumberModel>();
            vipNumberList = context.Vipnumber.Where(x => x.IsDeleted == false).Select(x => new VIPNumberModel()
            {
                Id = x.Id,
                VIPNumber = x.Number,
                SimtypeName = x.Simtype.Name
            }).ToList();
            return vipNumberList;
        }

        public VIPNumberModel GetVIPNumberById(int id)
        {
            var vipNumberList = new VIPNumberModel();
            vipNumberList = context.Vipnumber.Where(x => x.Id == id && x.IsDeleted == false).Select(x => new VIPNumberModel()
            {
                Id = x.Id,
                VIPNumber = x.Number,
                SimtypeName = x.Simtype.Name
            }).FirstOrDefault();
            return vipNumberList;
        }

        public void AddNewVIPNumber(VIPNumModel model)
        {
            context.Vipnumber.Add(new Vipnumber()
            {
                Number = model.VIPNumber,
                SimtypeId = model.SimTypeId,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            });
            context.SaveChanges();
        }

        public bool CheckVIPNumberExist(string number)
        {
            var isExist = false;
            isExist = context.Vipnumber.Any(x => x.Number == number);
            return isExist;
        }
    }
}
