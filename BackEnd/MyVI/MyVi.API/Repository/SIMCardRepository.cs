using MyVi.API.Entities;
using MyVi.API.IRepository;
using MyVi.API.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Repository
{
    public class SIMCardRepository : GenericRepository<OrderSIMCard>, ISIMCard
    {
        public SIMCardRepository(MyVIDBContext context) : base(context)
        {

        }

        // order new sim card means user can able to buy new sim card
        public void OrderNewSimCard(OrderSIMCard orderSIMCard)
        {
            var newAddress = new Address()
            {
                City = orderSIMCard.City,
                State = orderSIMCard.State,
                FlatNo = orderSIMCard.FlatNo,
                Country = orderSIMCard.Country,
                PincodeNo = orderSIMCard.PincodeNo,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            };
            context.Address.Add(newAddress);
            context.SaveChanges();

            var plan = context.Plan.Where(x => x.Id == orderSIMCard.PlanId).FirstOrDefault();

            context.Order.Add(new Order()
            {
                UserId = orderSIMCard.UserId,
                PlanId = orderSIMCard.PlanId,
                AlternateContactNo = orderSIMCard.AlternateContactNo,
                SimtypeId = orderSIMCard.SimtypeId,
                Quantity = HelperConstant.SIMQuantity,
                TotalPrice = (HelperConstant.SIMPrice * HelperConstant.SIMQuantity) + plan.Price,
                Status = orderSIMCard.Status,  //conformed=2
                AddessId = newAddress.Id,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            });
            context.SaveChanges();

        }

        //delete sim card order record
        public override void Delete(int id)
        {
            var registeredOrder = context.Order.Where(x => x.Id == id &&
                                                      x.VipnumberId == null &&
                                                      x.PortNumberId == null)
                                               .FirstOrDefault();
            registeredOrder.IsDeleted = true;
            registeredOrder.OnUpdated = DateTime.Now;
            context.SaveChanges();
        }

        //update sim card order details
        public override void Update(int id, OrderSIMCard entity)
        {
            var registeredOrder = context.Order.Where(x => x.Id == id &&
                                                           x.VipnumberId == null &&
                                                           x.PortNumberId == null).FirstOrDefault();
            registeredOrder.UserId = entity.UserId;
            registeredOrder.PlanId = entity.PlanId;
            registeredOrder.AlternateContactNo = entity.AlternateContactNo;
            registeredOrder.SimtypeId = entity.SimtypeId;
            registeredOrder.Status = entity.Status;  //conformed=2
            registeredOrder.AddessId = entity.Id;
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

        public bool CheckOrderIdExist(int orderId)
        {
            var isExist = false;
            isExist = context.Order.Any(x => x.Id == orderId);
            return isExist;
        }

        public IEnumerable<OrderModel> GetAllSimCardOrder()
        {
            var listOfSimCardOrder = new List<OrderModel>();

            listOfSimCardOrder = context.Order.Where(x => x.IsDeleted == false &&
                                                        x.VipnumberId == null &&
                                                        x.PortNumberId == null)
                .Select(x => new OrderModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    AlternateContactNo = x.AlternateContactNo,
                    SIMtypeName = x.Simtype.Name,
                    SIMtypeId = x.SimtypeId,
                    Quantity = x.Quantity,
                    TotalPrice = x.TotalPrice,
                    AddessId = x.AddessId,
                    Status = ((OrderStatus)x.Status).ToString()
                }).ToList();

            return listOfSimCardOrder;
        }

        public OrderModel GetSimCardOrderById(int id)
        {
            var SimCardOrder = new OrderModel();

            SimCardOrder = context.Order.Where(x => x.Id == id &&
                                                    x.IsDeleted == false &&
                                                    x.VipnumberId == null &&
                                                    x.PortNumberId == null)
                .Select(x => new OrderModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    AlternateContactNo = x.AlternateContactNo,
                    SIMtypeName = x.Simtype.Name,
                    SIMtypeId = x.SimtypeId,
                    Quantity = x.Quantity,
                    TotalPrice = x.TotalPrice,
                    AddessId = x.AddessId,
                    Status = ((OrderStatus)x.Status).ToString()
                }).FirstOrDefault();

            return SimCardOrder;
        }
    }
}
