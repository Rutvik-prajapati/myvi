using MyVi.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.IRepository
{
    public interface IOrder : IGenericInterface<Order>
    {
        public string NewOrder<T>(T obj);
    }
}
