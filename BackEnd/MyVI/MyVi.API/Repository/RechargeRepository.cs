using MyVi.API.Entities;
using MyVi.API.IRepository;
using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Repository
{
    public class RechargeRepository : GenericRepository<RechargeModel>, IRecharge
    {
        public RechargeRepository(MyVIDBContext context) : base(context)
        {

        }

        public override void Create(RechargeModel entity)
        {
            context.UserRechargeHistory.Add(new UserRechargeHistory()
            {
                UserId = entity.UserId,
                PlanId = entity.PlanId,
                RzpOrderId = entity.RzpOrderId,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            });
            context.SaveChanges();
        }

        public IEnumerable<RechargeDetailModel> GetAllRecharge()
        {
            throw new NotImplementedException();
        }

        public string GetRzpOrderId(RechargeModel rechargeModel)
        {
            var rechargeDetail = context.UserRechargeHistory.Where(x => x.UserId == rechargeModel.UserId && x.PlanId == rechargeModel.PlanId && x.IsDeleted == false)
                                                 .Select(x => new
                                                 {
                                                     orderId = x.RzpOrderId,
                                                     createdTime = x.OnCreated
                                                 }).OrderByDescending(x => x.createdTime).FirstOrDefault();
            return rechargeDetail.orderId;
        }

        public override void Update(int id, RechargeModel entity)
        {
            var rechargeRecord = context.UserRechargeHistory.Where(x => x.UserId == entity.UserId && x.PlanId == entity.PlanId && x.IsDeleted == false)
                                                            .OrderByDescending(x => x.OnCreated).FirstOrDefault();
            //rechargeRecord.UserId = entity.UserId;
            //rechargeRecord.PlanId = entity.PlanId;
            //rechargeRecord.RzpOrderId = entity.RzpOrderId;
            rechargeRecord.RzpPaymentId = entity.RzpPaymentId;
            rechargeRecord.RzpSignature = entity.RzpSignature;
            rechargeRecord.OnUpdated = DateTime.Now;
            context.SaveChanges();
        }

        ////public override IEnumerable<RechargeModel> GetAll()
        ////{
        ////    var rechargeList = new List<RechargeModel>();
        ////    rechargeList = context.UserRechargeHistory.Where(x=>x.IsDeleted == false)
        ////        .Select(x => new RechargeModel()
        ////    {
        ////        Id = x.Id,
        ////        UserId = x.UserId,
        ////        PlanId = x.PlanId,
        ////        CardNumber = x.PaymentCard.CardNumber,
        ////        Expiry = x.PaymentCard.Expiry,
        ////        Cvv = x.PaymentCard.Cvv
        ////    }).ToList();

        ////    return rechargeList;
        ////}

        //public override void Delete(int id)
        //{
        //    var rechargeDetail = context.UserRechargeHistory.Where(x => x.Id == id).FirstOrDefault();
        //    rechargeDetail.IsDeleted = true;
        //    rechargeDetail.OnUpdated = DateTime.Now;
        //    context.SaveChanges();
        //}

        //public RechargeDetailModel GetRechargeDetailById(int id)
        //{
        //    var recharge = new RechargeDetailModel();
        //    recharge = context.UserRechargeHistory.Where(x => x.IsDeleted == false && x.Id == id)
        //        .Select(x => new RechargeDetailModel()
        //        {
        //            Id = x.Id,
        //            UserId = x.UserId,
        //            ContactNo = x.User.ContactNo,
        //            PlanId = x.PlanId,
        //            PlanTypeId = x.Plan.PlanTypeId,
        //            Price = x.Plan.Price,
        //            Call = x.Plan.Call,
        //            Data = x.Plan.Data,
        //            Sms = x.Plan.Sms,
        //            Validity = x.Plan.Validity,
        //            Benefits = x.Plan.Benefits,
        //            CardNumber = x.PaymentCard.CardNumber,
        //            Expiry = x.PaymentCard.Expiry,
        //            Cvv = x.PaymentCard.Cvv
        //        }).FirstOrDefault();

        //    return recharge;
        //}

        //public IEnumerable<RechargeDetailModel> GetAllRecharge()
        //{
        //    var rechargeList = new List<RechargeDetailModel>();
        //    rechargeList = context.UserRechargeHistory.Where(x => x.IsDeleted == false)
        //        .Select(x => new RechargeDetailModel()
        //        {
        //            Id = x.Id,
        //            UserId = x.UserId,
        //            ContactNo = x.User.ContactNo,
        //            PlanId = x.PlanId,
        //            PlanTypeId = x.Plan.PlanTypeId,
        //            Price = x.Plan.Price,
        //            Call = x.Plan.Call,
        //            Data = x.Plan.Data,
        //            Sms = x.Plan.Sms,
        //            Validity = x.Plan.Validity,
        //            Benefits = x.Plan.Benefits,
        //            CardNumber = x.PaymentCard.CardNumber,
        //            Expiry = x.PaymentCard.Expiry,
        //            Cvv = x.PaymentCard.Cvv
        //        }).ToList();

        //    return rechargeList;
        //}

        //public IEnumerable<RechargeDetailModel> GetAllRechargeByUserId(int userId)
        //{
        //    var rechargeList = new List<RechargeDetailModel>();

        //    rechargeList = context.UserRechargeHistory.Where(x => x.UserId == userId && x.IsDeleted == false)
        //        .Select(x => new RechargeDetailModel()
        //        {
        //            Id = x.Id,
        //            UserId = x.UserId,
        //            ContactNo = x.User.ContactNo,
        //            PlanId = x.PlanId,
        //            PlanTypeId = x.Plan.PlanTypeId,
        //            Price = x.Plan.Price,
        //            Call = x.Plan.Call,
        //            Data = x.Plan.Data,
        //            Sms = x.Plan.Sms,
        //            Validity = x.Plan.Validity,
        //            Benefits = x.Plan.Benefits,
        //            CardNumber = x.PaymentCard.CardNumber,
        //            Expiry = x.PaymentCard.Expiry,
        //            Cvv = x.PaymentCard.Cvv
        //        }).ToList();

        //    return rechargeList;
        //}

        //public bool CheckRechargeIdExist(int planId)
        //{
        //    var isExist = false;
        //    isExist = context.UserRechargeHistory.Any(x => x.Id == planId);
        //    return isExist;
        //}
    }
}
