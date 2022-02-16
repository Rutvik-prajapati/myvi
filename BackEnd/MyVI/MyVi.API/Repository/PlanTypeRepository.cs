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
    public class PlanTypeRepository : GenericRepository<PlanTypeModel>, IPlanType
    {
        public PlanTypeRepository(MyVIDBContext context) : base(context)
        {

        }
        public override void Create(PlanTypeModel entity)
        {
            context.PlanType.Add(new PlanType()
            {
                SimTypeId = entity.SimTypeId,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            });
            context.SaveChanges();
        }

        public override void Update(int id, PlanTypeModel entity)
        {
            var planTypeDetail = context.PlanType.Where(x => x.Id == id).FirstOrDefault();
            planTypeDetail.Name = entity.Name;
            planTypeDetail.Description = entity.Description;
            planTypeDetail.OnUpdated = DateTime.Now;

            context.SaveChanges();
        }

        public IEnumerable GetAllPlanType()
        {
            List<PlanTypeModel> pt = new List<PlanTypeModel>();
            pt = context.PlanType.Select(x => new PlanTypeModel
            {
                Id = x.Id,
                SimTypeId = x.SimTypeId,
                Name = x.Name,
                Description = x.Description
            }).ToList();

            return pt;
        }
    }
}
