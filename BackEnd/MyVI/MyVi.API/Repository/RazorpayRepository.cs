
using Microsoft.Extensions.Options;
using MyVi.API.Entities;
using MyVi.API.IRepository;
using MyVi.API.Model;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyVi.API.Repository
{
    public class RazorpayRepository : GenericRepository<RazorpayOrderModel>, IRazorpay
    {
        private string _key;
        private string _secret;
        public RazorpayRepository(MyVIDBContext context, IOptions<RazorpaySetting> razorpaySettings) : base(context)
        {
            _key = razorpaySettings.Value.Key;
            _secret = razorpaySettings.Value.Secret;
        }

        public bool CompareSignatures(string orderId, string paymentId, string signature)
        {
            var text = orderId + "|" + paymentId;
            var secret = _secret;
            var generatedSignature = CalculateSHA256(text, secret);
            if (generatedSignature == signature)
                return true;
            else
                return false;
        }

        private string CalculateSHA256(string text, string secret)
        {
            string result = "";
            var enc = Encoding.Default;
            byte[]
            baText2BeHashed = enc.GetBytes(text),
            baSalt = enc.GetBytes(secret);
            System.Security.Cryptography.HMACSHA256 hasher = new HMACSHA256(baSalt);
            byte[] baHashedText = hasher.ComputeHash(baText2BeHashed);
            result = string.Join("", baHashedText.ToList().Select(b => b.ToString("x2")).ToArray());
            return result;
        }

        public string CreateNewOrder(decimal amount)
        {
            RazorpayOrderModel order = new RazorpayOrderModel()
            {
                OrderAmount = amount,
                Currency = "INR",
                Payment_Capture = 1,    // 0 - Manual capture, 1 - Auto capture
                Notes = new Dictionary<string, string>()
                {
                    { "note 1", "first note while creating order" }, { "note 2", "you can add max 15 notes" },
                    { "note for account 1", "this is a linked note for account 1" }, { "note 2 for second transfer", "it's another note for 2nd account" }
                }
            };
            var orderId = CreateOrder(order);
            return orderId;
        }

        private string CreateOrder(RazorpayOrderModel order)
        {
            try
            {
                RazorpayClient client = new RazorpayClient(_key, _secret);
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", order.OrderAmountInSubUnits);
                options.Add("currency", order.Currency);
                options.Add("payment_capture", order.Payment_Capture);
                options.Add("notes", order.Notes);

                Razorpay.Api.Order orderResponse = client.Order.Create(options);
                var orderId = orderResponse.Attributes["id"].ToString();
                return orderId;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
