using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyVi.API.Entities;
using MyVi.API.IRepository;
using MyVi.API.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Repository
{
    public class PlanRepository : GenericRepository<PlanModel>, IPlan
    {
        private IConfiguration configuration;
        public PlanRepository(MyVIDBContext context, IConfiguration _configuration) : base(context)
        {
            this.configuration = _configuration;
        }

        public override void Create(PlanModel entity)
        {
            context.Plan.Add(new Plan()
            {
                PlanTypeId = entity.PlanTypeId,
                Price = entity.Price,
                Talktime = entity.Talktime,
                Call = entity.Call,
                Data = entity.Data,
                Sms = entity.Sms,
                Validity = entity.Validity,
                Connection = entity.Connection,
                Benefits = entity.Benefits,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            });
            context.SaveChanges();
        }

        public override void Delete(int id)
        {
            var registeredPlan = context.Plan.Where(x => x.Id == id).FirstOrDefault();
            registeredPlan.IsDeleted = true;
            registeredPlan.OnUpdated = DateTime.Now;
            context.SaveChanges();
        }

        public override void Update(int id, PlanModel entity)
        {
            var registeredPlan = context.Plan.Where(x => x.Id == id).FirstOrDefault();
            registeredPlan.PlanTypeId = entity.PlanTypeId;
            registeredPlan.Talktime = entity.Talktime;
            registeredPlan.Price = entity.Price;
            registeredPlan.Call = entity.Call;
            registeredPlan.Data = entity.Data;
            registeredPlan.Sms = entity.Sms;
            registeredPlan.Validity = entity.Validity;
            registeredPlan.Connection = entity.Connection;
            registeredPlan.Benefits = entity.Benefits;
            registeredPlan.OnUpdated = DateTime.Now;
            context.SaveChanges();
        }
        public IEnumerable<PlanDetailModel> GetAllPlan()
        {
            var planList = new List<PlanDetailModel>();

            planList = (from p in context.Plan
                        join pt in context.PlanType on p.PlanTypeId equals pt.Id into planInfo
                        from plan in planInfo.DefaultIfEmpty()
                        where p.IsDeleted == false && plan.IsDeleted == false
                        select new PlanDetailModel()
                        {
                            Id = p.Id,
                            PlanTypeName = plan.Name,
                            Price = p.Price,
                            Talktime = p.Talktime,
                            Call = p.Call,
                            Data = p.Data,
                            Sms = p.Sms,
                            Validity = p.Validity,
                            Connection = p.Connection,
                            Benefits = p.Benefits
                        }).ToList();
            return planList;
        }

        public PlanDetailModel GetPlanById(int id)
        {
            var planDetail = new PlanDetailModel();
            planDetail = (from p in context.Plan
                          join pt in context.PlanType on p.PlanTypeId equals pt.Id into planInfo
                          from plan in planInfo.DefaultIfEmpty()
                          where p.Id == id
                          select new PlanDetailModel()
                          {
                              Id = p.Id,
                              PlanTypeName = plan.Name,
                              Price = p.Price,
                              Talktime = p.Talktime,
                              Call = p.Call,
                              Data = p.Data,
                              Sms = p.Sms,
                              Validity = p.Validity,
                              Connection = p.Connection,
                              Benefits = p.Benefits
                          }).FirstOrDefault();
            return planDetail;
        }

        public bool CheckPlanExist(int planId)
        {
            var isExist = false;
            isExist = context.Plan.Any(x => x.Id == planId);
            return isExist;
        }

        public IEnumerable<PlanDetailModel> GetPlanListByPlantypeId(int planTypeId)
        {
            var planList = new List<PlanDetailModel>();
            planList = context.Plan.Where(x => x.IsDeleted == false &&
                                             x.PlanTypeId == planTypeId)
            .Select(x => new PlanDetailModel()
            {
                Id = x.Id,
                Price = x.Price,
                Talktime = x.Talktime,
                Call = x.Call,
                Data = x.Data,
                Sms = x.Sms,
                Validity = x.Validity,
                Connection = x.Connection,
                Benefits = x.Benefits
            }).ToList();
            return planList;
        }

        public IEnumerable<PlanDetailModel> GetPlanListBySIMTypeId(int simTypeId)
        {
            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("ConnStr")))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_GetPlanBySimTypeId", con);
                cmd.Parameters.AddWithValue("@simTypeId", simTypeId);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Create output parameter. "-1" is used for nvarchar(max)
                cmd.Parameters.Add("@jsonOutput", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                // Execute the command
                cmd.ExecuteNonQuery();

                // Get the values
                string json = cmd.Parameters["@jsonOutput"].Value.ToString();
                List<PlanDetailModel> res = JsonConvert.DeserializeObject<List<PlanDetailModel>>(json);

                con.Close();

                return res;
            }
        }
    }
}
