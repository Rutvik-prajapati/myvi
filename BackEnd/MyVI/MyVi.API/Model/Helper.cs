using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Model
{
    public static class HelperConstant
    {
        public static int SIMQuantity = 1;
        public static int SIMPrice = 50;
    }

    enum OrderStatus
    {
        Pending = 1,
        Conformed = 2,
        Canceled = 3,
        Shipped = 4,
        OutOfDelivery = 5,
        Delivered = 6
    }
    enum PlanTypeEnum
    {
        Prepaid = 1,
        Postpaid
    }
}
