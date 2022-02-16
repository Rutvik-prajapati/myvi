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
    public class RechargeController : Controller
    {
        private readonly IRecharge recharge;
        private readonly IUser user;
        private readonly IPlan plan;
        private readonly IRazorpay razorpay;
        public RechargeController(IRecharge context, IUser _user, IPlan _plan,IRazorpay _razorpay)
        {
            recharge = context;
            user = _user;
            plan = _plan;
            razorpay = _razorpay;
        }

        // POST: api/myvi/Recharge/NewRecharge
        [Authorize]
        [HttpPost]
        [Route("NewRecharge")]
        public ActionResult<IEnumerable> NewRecharge(RechargeModel rechargeModel )
        { 
            var orderId = razorpay.CreateNewOrder(rechargeModel.Price);
            if (orderId != null)
            {
                rechargeModel.RzpOrderId = orderId;
                recharge.Create(rechargeModel);
                return Ok(new { Status = "success", OrderId = orderId });
            }
            return Ok(new { Status = "fail", OrderId = orderId });
        }

        [Authorize]
        [HttpPost]
        [Route("checkout")]
        public ActionResult<IEnumerable> Checkout(RechargeModel rechargeModel)
        {
            var orderId = recharge.GetRzpOrderId(rechargeModel);
            var validSignature = razorpay.CompareSignatures(orderId, rechargeModel.RzpPaymentId, rechargeModel.RzpSignature);
            if (validSignature == true)
            {
                rechargeModel.RzpOrderId = orderId;
                recharge.Update(rechargeModel.Id, rechargeModel);
                return Ok(new { Status = "success", message = true });
            }
            else
                return Ok(new { Status = "fail", message = false });
        }
    }
}
