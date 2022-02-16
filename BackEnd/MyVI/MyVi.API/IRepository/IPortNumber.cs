using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.IRepository
{
    public interface IPortNumber : IGenericInterface<OrderPortNumber>
    {
        public string OrderPortNumber(OrderPortNumber orderPortNumber);
        public IEnumerable<OrderModel> GetAllOrderedPortNumber();
        public OrderModel GetOrderedPortNumberById(int id);
        public string CheckPortNumberExist(string number);
    }
}
