//using MyVi.API.Entities;
using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.IRepository
{
    public interface ISIMCard : IGenericInterface<OrderSIMCard>
    {
        public void OrderNewSimCard(OrderSIMCard orderSIMCard);
        public bool CheckOrderIdExist(int orderId);
        public IEnumerable<OrderModel> GetAllSimCardOrder();
        public OrderModel GetSimCardOrderById(int id);
    }
}
