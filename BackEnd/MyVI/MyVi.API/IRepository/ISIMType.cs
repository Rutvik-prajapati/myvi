using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.IRepository
{
    public interface ISIMType : IGenericInterface<SIMTypeModel>
    {
        public bool CheckSIMTypeExist(int simTypeId);
        public SIMTypeModel GetSIMTypeById(int id);
    }
}
