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
    public class SIMTypeController : Controller
    {
        private readonly ISIMType simType;

        public SIMTypeController(ISIMType context)
        {
            simType = context;
        }

        // POST: api/myvi/SIMType/AddNewSIMType
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddNewSIMType")]
        public ActionResult<IEnumerable> CreateNewSIMType([FromBody] SIMTypeModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
                return BadRequest("Invalid data.");

            simType.Create(model);

            return Ok(new { Status = "Success", Message = "Successfully created Simtype" });
        }

        // GET: api/myvi/SIMType/UpdateSimTypeDetail
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("UpdateSimTypeDetail")]
        public ActionResult<IEnumerable> UpdateSIMType(int id,[FromBody]SIMTypeModel model)
        {
            if (id <= 0 || string.IsNullOrEmpty(model.Name))
                return BadRequest("Invalid data.");

            simType.Update(id, model);

            return Ok(new { Status = "Success", Message = "Successfully updated SIMtype" });
        }

        // Delete: api/myvi/SIMType/DeleteSimType
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteSimType")]
        public ActionResult<IEnumerable> DeleteSIMType(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid data.");

            simType.Delete(id);

            return Ok(new { Status = "Success", Message = "Successfully delete SIMtype" });
        }

        // GET: api/myvi/SIMType/GetAllSimType
        //[Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("GetAllSimType")]
        public ActionResult<IEnumerable> GetAllSIMType()
        {
            var simTypeList = simType.GetAll();

            if (simTypeList == null)
            {
                return NotFound(simTypeList);
            }

            return Ok(simTypeList);
        }

        // GET: api/myvi/SIMType/GetSimTypeById
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetSimTypeById")]
        public ActionResult<IEnumerable> GetSIMTypeById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid data");
           
            var simTypeDetail = simType.GetById(id);

            if (simTypeDetail == null)
            {
                return NotFound(simTypeDetail);
            }

            return Ok(simTypeDetail);
        }
    }
}
