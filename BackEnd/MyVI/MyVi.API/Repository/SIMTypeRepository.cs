using MyVi.API.Entities;
using MyVi.API.IRepository;
using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Repository
{
    public class SIMTypeRepository : GenericRepository<SIMTypeModel>, ISIMType
    {
        public SIMTypeRepository(MyVIDBContext context) : base(context)
        {

        }

        public override void Create(SIMTypeModel entity)
        {
            context.Simtype.Add(new Simtype()
            {
                Name = entity.Name,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            });
            context.SaveChanges();
        }

        public override void Update(int id, SIMTypeModel entity)
        {
            var simTypeDetail = context.Simtype.Where(x => x.Id == id &&
                                                           x.IsDeleted == false)
                                                .FirstOrDefault();
            simTypeDetail.Name = entity.Name;
            simTypeDetail.OnUpdated = DateTime.Now;
            context.SaveChanges();
        }
        public override IEnumerable<SIMTypeModel> GetAll()
        {
            var simTypeList = new List<SIMTypeModel>();
            simTypeList = context.Simtype.Where(x => x.IsDeleted == false)
                .Select(x => new SIMTypeModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            return simTypeList;
        }

        public override void Delete(int id)
        {
            var simTypeDetail = context.Simtype.FirstOrDefault(x => x.Id == id);
            simTypeDetail.IsDeleted = true;
            context.SaveChanges();
        }

        public bool CheckSIMTypeExist(int simTypeId)
        {
            var isExist = false;
            isExist = context.Simtype.Any(x => x.Id == simTypeId);
            return isExist;
        }

        public SIMTypeModel GetSIMTypeById(int id)
        {
            var simTypeDetail = new SIMTypeModel();

            simTypeDetail = context.Simtype.Where(x => x.IsDeleted == false)
                .Select(x => new SIMTypeModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).FirstOrDefault(x => x.Id == id);

            return simTypeDetail;
        }
    }
}
