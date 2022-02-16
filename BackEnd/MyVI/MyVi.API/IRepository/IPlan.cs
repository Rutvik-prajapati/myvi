using Microsoft.Data.SqlClient;
using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.IRepository
{
    public interface IPlan : IGenericInterface<PlanModel>
    {
        public IEnumerable<PlanDetailModel> GetAllPlan();
        public PlanDetailModel GetPlanById(int id);
        public bool CheckPlanExist(int planId);
        public IEnumerable<PlanDetailModel> GetPlanListByPlantypeId(int planTypeId);
        public IEnumerable<PlanDetailModel> GetPlanListBySIMTypeId(int simTypeId);
    }
}
