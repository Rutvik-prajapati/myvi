using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.IRepository
{
    public interface IRecharge : IGenericInterface<RechargeModel>
    {
        public IEnumerable<RechargeDetailModel> GetAllRecharge();
        //public RechargeDetailModel GetRechargeDetailById(int id);
        //public IEnumerable<RechargeDetailModel> GetAllRechargeByUserId(int userId);
        //public bool CheckRechargeIdExist(int id);
        public string GetRzpOrderId(RechargeModel rechargeModel);
    }
}
