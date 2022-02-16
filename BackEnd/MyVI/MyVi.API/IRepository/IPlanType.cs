//using MyVi.API.Entities;
using MyVi.API.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.IRepository
{
    public interface IPlanType : IGenericInterface<PlanTypeModel>
    {
        public IEnumerable GetAllPlanType();
    }
}
