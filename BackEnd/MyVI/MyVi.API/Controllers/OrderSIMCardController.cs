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
    public class OrderSIMCardController : Controller
    {
        private readonly ISIMCard simCard;
        private readonly IAddress address;
        private readonly IUser user;
        private readonly IPlan plan;
        private readonly ISIMType simType;
        private readonly IOrder order;

        public OrderSIMCardController(ISIMCard orderSIMCard,
            IAddress addressContext, IUser _user, IPlan _plan,
            ISIMType _simType, IOrder _order)
        {
            simCard = orderSIMCard;
            address = addressContext;
            user = _user;
            plan = _plan;
            simType = _simType;
            order = _order;
        }

        // POST: api/myvi/OrderSIMCard/BuyNewSIM
        [Authorize]
        [HttpPost]
        [Route("BuyNewSIM")]
        public ActionResult<IEnumerable> BuyNewSimCard([FromBody] OrderSIMCard model)
        {
            var result = "";
            if (model.UserId <= 0 || model.PlanId <= 0 ||
                string.IsNullOrEmpty(model.AlternateContactNo) ||
                model.SimtypeId <= 0 || model.Status <= 0 ||
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
                //simCard.OrderNewSimCard(model); 
                //OR
                result = order.NewOrder(model);  //using sp call

                if (result == "")
                    return Ok(new { Status = "success", Message = "Successfully place order" });

            }
            return Ok(new { Status = "fail", Message = result });
        }

        // DELETE: api/myvi/OrderSIMCard/DeleteSimCardOrder
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteSimCardOrder")]
        public ActionResult<IEnumerable> DeleteSIMCardOrder(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid data.");

            simCard.Delete(id);

            return Ok(new { Status = "success", Message = "Successfully delete Sim card Order" });
        }

        // GET: api/myvi/OrderSIMCard/GetAllSimCardOrder
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllSimCardOrder")]
        public ActionResult<IEnumerable> GetAllSIMOrder()
        {
            var orderList = simCard.GetAllSimCardOrder();

            if (orderList == null)
            {
                return NotFound(orderList);
            }

            return Ok(orderList);
        }

        // GET: api/myvi/OrderSIMCard/GetSimOrderById
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetSimOrderById")]
        public ActionResult<IEnumerable> GetSIMOrderById(int id)
        {
            var orderDetail = new OrderModel();
            if (id <= 0)
                return BadRequest("Invalid Data");

            else if (simCard.CheckOrderIdExist(id))
                orderDetail = simCard.GetSimCardOrderById(id);

            else
                return BadRequest(new { Status = "fail", Message = "OrderId does not exist" });

            if (orderDetail == null)
            {
                return NotFound(orderDetail);
            }

            return Ok(orderDetail);
        }

        // POST: api/myvi/OrderSIMCard/UpdateSimOrderDetail
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("UpdateSimOrderDetail")]
        public ActionResult<IEnumerable> UpdateSIMOrderDetail(int id, [FromBody] OrderSIMCard model)
        {
            if (model.UserId <= 0 || model.PlanId <= 0 ||
                model.AddessId <= 0 || string.IsNullOrEmpty(model.AlternateContactNo) ||
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
            else if (address.CheckAddressIdExist(model.AddessId) == false)
                return Ok(new { Status = "fail", Message = "AddressId does not exist" });
            else
                simCard.Update(id, model);


            return Ok(new { Status = "success", Message = "Successfully updated sim order detail" });
        }
    }
}

