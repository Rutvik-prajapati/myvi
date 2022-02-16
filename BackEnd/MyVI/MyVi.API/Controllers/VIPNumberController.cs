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
    public class VIPNumberController : Controller
    {
        private readonly ISIMCard simCard;
        private readonly IPortNumber orderPortNumber;
        private readonly IAddress address;
        private readonly IUser user;
        private readonly IPlan plan;
        private readonly ISIMType simType;
        private readonly IVIPNumber vipNumber;
        private readonly IOrder order;
        public VIPNumberController(IPortNumber context, IAddress addressContext, IUser _user, IPlan _plan,
            ISIMType _simType, ISIMCard _simCard, IVIPNumber _vipNumber, IOrder _order)
        {
            simCard = _simCard;
            orderPortNumber = context;
            address = addressContext;
            user = _user;
            plan = _plan;
            simType = _simType;
            vipNumber = _vipNumber;
            order = _order;
        }

        // POST: api/myvi/VIPNumber/OrderVIPNumber
        [Authorize]
        [HttpPost]
        [Route("OrderVIPNumber")]
        public ActionResult<IEnumerable> BuyNewVIPNumber([FromBody] OrderVIPNumber model)
        {
            var result = "";
            if (model.UserId <= 0 || model.PlanId <= 0 ||
                string.IsNullOrEmpty(model.AlternateContactNo) ||
                model.SimtypeId <= 0 || model.Status <= 0 ||
                model.VIPNumberId <= 0 ||
                string.IsNullOrEmpty(model.City) || string.IsNullOrEmpty(model.State) ||
                string.IsNullOrEmpty(model.Country) || string.IsNullOrEmpty(model.PincodeNo) ||
                model.FlatNo <= 0)
                return BadRequest("Invalid data.");

            if (user.CheckUserExist(model.UserId) == false)
                return Ok(new { Status = "fail", Message = "UserId does not exist" });
            else if (plan.CheckPlanExist(model.PlanId) == false)
                return Ok(new { Status = "fail", Message = "PlanId does not exist" });
            else if (simType.CheckSIMTypeExist(model.SimtypeId) == false)
                return Ok(new { Status = "fail", Message = "SimTypeId does not exist" });
            else if (vipNumber.CheckVIPNumberIdExist(model.VIPNumberId) == false)
                return Ok(new { Status = "fail", Message = "VIPNumberId does not exist" });
            else
            {
                //vipNumber.Create(model); 
                //OR
                result = order.NewOrder(model);  //using sp call

                if (result == "")
                    return Ok(new { Status = "success", Message = "Successfully place order" });
            }

            return Ok(new { Status = "fail", Message = result });
        }

        // DELETE: api/myvi/VIPNumber/DeleteVIPNumberOrder
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteVIPNumberOrder")]
        public ActionResult<IEnumerable> DeletePortNumberOrder(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid data.");

            vipNumber.Delete(id);

            return Ok("Successfully delete vipNumber Order");
        }

        // GET: api/myvi/VIPNumber/GetAllvipNumberOrder
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllvipNumberOrder")]
        public ActionResult<IEnumerable> GetAllVIPNumberOrder()
        {
            var orderList = vipNumber.GetAllOrderedVIPNumber();

            if (orderList == null)
            {
                return NotFound(orderList);
            }

            return Ok(orderList);
        }

        // GET: api/myvi/VIPNumber/GetvipNumberOrderById
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetvipNumberOrderById")]
        public ActionResult<IEnumerable> GetVIPNumberOrderById(int id)
        {
            var orderDetail = new OrderModel();
            if (id <= 0)
                return BadRequest("Invalid Data");

            else if (simCard.CheckOrderIdExist(id))
                orderDetail = vipNumber.GetOrderedVIPNumberById(id);

            else
                return BadRequest("OrderId does not exist");

            if (orderDetail == null)
            {
                return NotFound(orderDetail);
            }

            return Ok(orderDetail);
        }

        // POST: api/myvi/VIPNumber/UpdateVIPNumberOrder
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("UpdateVIPNumberOrder")]
        public ActionResult<IEnumerable> UpdateVIPNumberOrderDetail(int id, [FromBody] OrderVIPNumber model)
        {
            if (model.UserId <= 0 || model.PlanId <= 0 ||
                string.IsNullOrEmpty(model.AlternateContactNo) || model.VIPNumberId <= 0 ||
                model.SimtypeId <= 0 || model.Status <= 0 ||
                string.IsNullOrEmpty(model.City) || string.IsNullOrEmpty(model.State) ||
                string.IsNullOrEmpty(model.Country) || string.IsNullOrEmpty(model.PincodeNo) ||
                model.FlatNo <= 0 || id <= 0)
                return BadRequest("Invalid data.");

            if (user.CheckUserExist(model.UserId) == false)
                return Ok(new { Status = "fail", Message = "UserId does not exist" });
            else if (plan.CheckPlanExist(model.PlanId) == false)
                return Ok(new { Status = "fail", Message = "PlanId does not exist" });
            else if (simType.CheckSIMTypeExist(model.SimtypeId) == false)
                return Ok(new { Status = "fail", Message = "SimTypeId does not exist" });
            else if (vipNumber.CheckVIPNumberIdExist(model.VIPNumberId) == false)
                return Ok(new { Status = "fail", Message = "VIPNumberId does not exist" });
            else
                vipNumber.Update(id, model);

            return Ok(new { Status = "success", Message = "Successfully updated vip number order detail" });
        }


        //get all vip numbers list which exist in db

        // GET: api/myvi/VIPNumber/GetAllvipNumber
        [Authorize]
        [HttpGet]
        [Route("GetAllvipNumber")]
        public ActionResult<IEnumerable> GetAllVIPNumber()
        {
            var vipNumList = vipNumber.GetAllVIPNumbers();

            if (vipNumList == null)
            {
                return NotFound(vipNumList);
            }

            return Ok(vipNumList);
        }

        // GET: api/myvi/VIPNumber/GetvipNumberById
        [Authorize]
        [HttpGet]
        [Route("GetvipNumberById")]
        public ActionResult<IEnumerable> GetVIPNumberById(int id)
        {
            var vipNum = new VIPNumberModel();
            if (id <= 0)
                return BadRequest("Invalid Data");

            else if (vipNumber.CheckVIPNumberIdExist(id))
                vipNum = vipNumber.GetVIPNumberById(id);

            else
                return BadRequest("VIPNumberId does not exist");

            if (vipNum == null)
            {
                return NotFound(vipNum);
            }

            return Ok(vipNum);
        }

        // POST: api/myvi/VIPNumber/AddNewVipNumber
        [Authorize]
        [HttpPost]
        [Route("AddNewVipNumber")]
        public ActionResult<IEnumerable> CreateNewVIPNumber([FromBody] VIPNumModel model)
        {
            if (model.SimTypeId <= 0 ||
                string.IsNullOrEmpty(model.VIPNumber))
                return BadRequest("Invalid data.");

            if (simType.CheckSIMTypeExist(model.SimTypeId) == false)
                return Ok(new { Status = "fail", Message = "SimTypeId does not exist" });
            if (vipNumber.CheckVIPNumberExist(model.VIPNumber) == true)
                return Ok(new { Status = "fail", Message = "vip number already exist" });

            vipNumber.AddNewVIPNumber(model);
            return Ok(new { Status = "success", Message = "Successfully add new vip number" });
        }

    }
}
