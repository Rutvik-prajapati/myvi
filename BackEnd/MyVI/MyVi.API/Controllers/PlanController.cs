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
    public class PlanController : Controller
    {
        private readonly IPlan plan;
        private readonly ISIMType simType;

        public PlanController(IPlan context,ISIMType _simType)
        {
            plan= context;
            simType = _simType;
        }
        // POST: api/myvi/Plan/AddNewPlan
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddNewPlan")]
        public ActionResult<IEnumerable> CreateNewPlan([FromBody] PlanModel model)
        {
            if (model.PlanTypeId <= 0)
                return BadRequest("Invalid data.");

            plan.Create(model);

            return Ok(new {Status="Success",Message="Successfully created plan" });
        }

        // POST: api/myvi/Plan/UpdatePlan
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("UpdatePlan")]
        public ActionResult<IEnumerable> UpdatePlan(int id,[FromBody] PlanModel model)
        {
            if (model.PlanTypeId <= 0 || id <= 0)
                return BadRequest("Invalid data.");

            plan.Update(id,model);

            return Ok(new { Status = "Success", Message = "Successfully updated plan" });
        }

        // POST: api/myvi/Plan/DeletePlan
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeletePlan")]
        public ActionResult<IEnumerable> DeletePlan(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid data.");

            plan.Delete(id);

            return Ok(new { Status = "Success", Message = "Successfully delete plan" });
        }

        // GET: api/myvi/Plan/GetAllPlan
        //[Authorize]
        [HttpGet]
        [Route("GetAllPlan")]
        public ActionResult<IEnumerable> GetAllPlan()
        {
            var planList = plan.GetAllPlan();

            if (planList == null)
            {
                return NotFound(planList);
            }

            return Ok(planList);
        }

        // GET: api/myvi/Plan/GetPlanById
        //[Authorize]
        [HttpGet]
        [Route("GetPlanById")]
        public ActionResult<IEnumerable> GetPlanById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Data");

            var planDetail = plan.GetPlanById(id);

            if (planDetail == null)
            {
                return NotFound(planDetail);
            }

            return Ok(planDetail);
        }

        // GET: api/myvi/Plan/GetPlanListByPlanTypeId
        //[Authorize]
        [HttpPost]
        [Route("GetPlanListByPlanTypeId")]
        public ActionResult<IEnumerable> GetPlanListByPlanTypeId(int planTypeId)
        {
            if (planTypeId <=0)
                return BadRequest("Invalid Data");

            var planDetail = plan.GetPlanListByPlantypeId(planTypeId);

            if (planDetail == null)
            {
                return NotFound(planDetail);
            }

            return Ok(planDetail);
        }

        // GET: api/myvi/Plan/GetPlanListBySIMTypeId
        //[Authorize]
        [HttpPost]
        [Route("GetPlanListBySIMTypeId")]
        public ActionResult<IEnumerable> GetPlanListBySimTypeId(int simTypeId)
        {
            if (simTypeId <= 0)
                return BadRequest("Invalid Data");
            if(simType.CheckSIMTypeExist(simTypeId) == false)
                return BadRequest("SIMTypeId does not exist");
            

            var planList = plan.GetPlanListBySIMTypeId(simTypeId);

            if (planList == null)
            {
                return NotFound(planList);
            }

            return Ok(planList);
        }
    }
}
