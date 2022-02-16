using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Model
{
    public class PlanTypeModel
    {
        public int Id { get; set; }
        public int SimTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
