using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.IRepository
{
    public interface IVIPNumber : IGenericInterface<OrderVIPNumber>
    {
        public IEnumerable<OrderModel> GetAllOrderedVIPNumber();
        public OrderModel GetOrderedVIPNumberById(int id);
        public bool CheckVIPNumberIdExist(int id);
        public IEnumerable<VIPNumberModel> GetAllVIPNumbers();
        public VIPNumberModel GetVIPNumberById(int vipNumId);
        public void AddNewVIPNumber(VIPNumModel model);
        public bool CheckVIPNumberExist(string number);
    }
}
