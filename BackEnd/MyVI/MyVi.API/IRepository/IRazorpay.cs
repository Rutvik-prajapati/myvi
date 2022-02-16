using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.IRepository
{
    public interface IRazorpay : IGenericInterface<RazorpayOrderModel>
    {
        public string CreateNewOrder(decimal amount);
        public bool CompareSignatures(string orderId, string paymentId, string signature);
    }
}
