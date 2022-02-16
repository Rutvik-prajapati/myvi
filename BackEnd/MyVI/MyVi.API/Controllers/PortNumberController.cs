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
    public class PortNumberController : Controller
    {
        private readonly ISIMCard simCard;
        private readonly IPortNumber orderPortNumber;
        private readonly IAddress address;
        private readonly IUser user;
        private readonly IPlan plan;
        private readonly ISIMType simType;
        private readonly IOrder order;
        public PortNumberController(IPortNumber context, IAddress addressContext, IUser _user, IPlan _plan,
            ISIMType _simType, ISIMCard _simCard, IOrder _order)
        {
            simCard = _simCard;
            orderPortNumber = context;
            address = addressContext;
            user = _user;
            plan = _plan;
            simType = _simType;
            order = _order;
        }

        // POST: api/myvi/PortNumber/OrderPortNumber
        [Authorize]
        [HttpPost]
        [Route("OrderPortNumber")]
        public ActionResult<IEnumerable> BuyNewPortSimCard([FromBody] OrderPortNumber model)
        {
            var result = "";
            if (model.UserId <= 0 || model.PlanId <= 0 ||
                string.IsNullOrEmpty(model.AlternateContactNo) ||
                model.SimtypeId <= 0 || model.Status <= 0 ||
                string.IsNullOrEmpty(model.PortNumber) ||
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
            else
            {
                //orderPortNumber.Create(model); 
                //OR
                result = order.NewOrder(model);  //using sp call

                if (result == "")
                    return Ok(new { Status = "success", Message = "Successfully place order" });
            }

            return Ok(new { Status = "fail", Message = result });
        }


        // DELETE: api/myvi/PortNumber/DeletePortNumberOrder
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeletePortNumberOrder")]
        public ActionResult<IEnumerable> DeletePortNumberOrder(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid data.");

            orderPortNumber.Delete(id);

            return Ok(new { Status = "success", Message = "Successfully delete Sim card Order" });
        }

        // GET: api/myvi/PortNumber/GetAllPortNumberOrder
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllPortNumberOrder")]
        public ActionResult<IEnumerable> GetAllPortOrder()
        {
            var orderList = orderPortNumber.GetAllOrderedPortNumber();

            if (orderList == null)
            {
                return NotFound(orderList);
            }

            return Ok(orderList);
        }

        // GET: api/myvi/PortNumber/GetPortNumberOrderById
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetPortNumberOrderById")]
        public ActionResult<IEnumerable> GetPortOrderById(int id)
        {
            var orderDetail = new OrderModel();
            if (id <= 0)
                return BadRequest("Invalid Data");

            else if (simCard.CheckOrderIdExist(id))
                orderDetail = orderPortNumber.GetOrderedPortNumberById(id);

            else
                return BadRequest("OrderId does not exist");

            if (orderDetail == null)
            {
                return NotFound(orderDetail);
            }

            return Ok(orderDetail);
        }

        // POST: api/myvi/PortNumber/UpdatePortOrderDetail
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("UpdatePortOrderDetail")]
        public ActionResult<IEnumerable> UpdatePortOrderDetail(int id, [FromBody] OrderPortNumber model)
        {
            if (model.UserId <= 0 || model.PlanId <= 0 ||
                string.IsNullOrEmpty(model.AlternateContactNo) || string.IsNullOrEmpty(model.PortNumber) ||
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
            else
                orderPortNumber.Update(id, model);

            return Ok(new { Status = "success", Message = "Successfully updated port order detail" });
        }

        // POST: api/myvi/PortNumber/CheckPortNumberExist
        [Authorize]
        [HttpPost]
        [Route("CheckPortNumberExist")]
        public ActionResult<IEnumerable> CheckPortNumberExist(string number)
        {
            if (string.IsNullOrEmpty(number) || number.Length < 10)
                return BadRequest("Invalid data.");

            var num = orderPortNumber.CheckPortNumberExist(number);

            if (num != null)
                return Ok(new { Status = "success", PortNumber = num });
            else
                return Ok(new { Status = "fail", PortNumber = num });

        }
    }
}
