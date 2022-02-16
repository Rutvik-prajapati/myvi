using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyVi.API.IRepository;
using MyVi.API.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Controllers
{
    [Route("api/myvi/[controller]")]
    [ApiController]
    public class PlanTypeController : Controller
    {
        private readonly IPlanType planType;

        public PlanTypeController(IPlanType context)
        {
            planType = context;
        }

        // POST: api/myvi/PlanType/AddNewPlanType
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddNewPlanType")]
        public ActionResult<IEnumerable> CreateNewPlanType([FromBody]PlanTypeModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Description) || model.SimTypeId <= 0)
                return BadRequest("Invalid data.");

            planType.Create(model);

            return Ok(new { Status = "Success", Message = "Successfully created plantype" });
        }

        // GET: api/myvi/PlanType/GetAllPlanType
        //[Authorize]
        [HttpGet]
        [Route("GetAllPlanType")]
        public ActionResult<IEnumerable> GetAllPlanTypeList()
        {
            var planTypeList = planType.GetAllPlanType();

            if (planTypeList == null)
            {
                return NotFound();
            }

            return Ok(planTypeList);
        }


        // POST: api/myvi/PlanType/updatePlantypeDetail
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("updatePlantypeDetail")]
        public ActionResult<IEnumerable> UpdatePlanTypeDetail(int id,[FromBody] PlanTypeModel model)
        {
            if (id<=0 || string.IsNullOrEmpty(model.Description))
                return BadRequest("Invalid data.");

            planType.Update(id,model);
            return Ok(new { Status = "Success", Message = "Update successfully" });
        }
    }
}
