using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyVi.API.Entities;
using MyVi.API.IRepository;
using MyVi.API.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Repository
{
    public class PortNumberRepository : GenericRepository<OrderPortNumber>, IPortNumber
    {
        private IConfiguration configuration;
        public PortNumberRepository(MyVIDBContext context, IConfiguration _configuration) : base(context)
        {
            configuration = _configuration;
        }

        public override void Create(OrderPortNumber entity)
        {
            var newAddress = new Address()
            {
                City = entity.City,
                State = entity.State,
                FlatNo = entity.FlatNo,
                Country = entity.Country,
                PincodeNo = entity.PincodeNo,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            };
            context.Address.Add(newAddress);
            context.SaveChanges();

            var newportNumber = new PortNumber()
            {
                Number = entity.PortNumber,
                SimtypeId = entity.SimtypeId,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            };
            context.PortNumber.Add(newportNumber);
            context.SaveChanges();


            var plan = context.Plan.Where(x => x.Id == entity.PlanId).FirstOrDefault();

            context.Order.Add(new Order()
            {
                UserId = entity.UserId,
                PlanId = entity.PlanId,
                AlternateContactNo = entity.AlternateContactNo,
                SimtypeId = entity.SimtypeId,
                Quantity = HelperConstant.SIMQuantity,
                TotalPrice = (HelperConstant.SIMPrice * HelperConstant.SIMQuantity) + plan.Price,
                Status = entity.Status,  //conformed=2
                AddessId = newAddress.Id,
                PortNumberId = newportNumber.Id,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            });
            context.SaveChanges();
        }

        //delete port number order from ordertable
        public override void Delete(int id)
        {
            var registeredOrder = context.Order.Where(x => x.Id == id &&
                                                      x.VipnumberId == null)
                                               .FirstOrDefault();
            registeredOrder.IsDeleted = true;
            registeredOrder.OnUpdated = DateTime.Now;
            context.SaveChanges();
        }

        //update portnumber order in ordertable
        public override void Update(int id, OrderPortNumber entity)
        {
            var registeredOrder = context.Order.Where(x => x.Id == id &&
                                                           x.VipnumberId == null).FirstOrDefault();
            registeredOrder.UserId = entity.UserId;
            registeredOrder.PlanId = entity.PlanId;
            registeredOrder.AlternateContactNo = entity.AlternateContactNo;
            registeredOrder.SimtypeId = entity.SimtypeId;
            registeredOrder.Status = entity.Status;  //conformed=2
            registeredOrder.AddessId = entity.Id;
            registeredOrder.OnUpdated = DateTime.Now;
            context.SaveChanges();

            var portNumberDetail = context.PortNumber.Where(x => x.Id == registeredOrder.PortNumberId &&
                                                                 x.IsDeleted == false)
                                                     .FirstOrDefault();
            portNumberDetail.Number = entity.PortNumber;
            portNumberDetail.SimtypeId = entity.SimtypeId;
            portNumberDetail.OnUpdated = DateTime.Now;
            context.SaveChanges();

            var orderAddress = context.Address.Where(x => x.Id == registeredOrder.AddessId).FirstOrDefault();
            orderAddress.City = entity.City;
            orderAddress.Country = entity.Country;
            orderAddress.FlatNo = entity.FlatNo;
            orderAddress.State = entity.State;
            orderAddress.PincodeNo = entity.PincodeNo;
            orderAddress.OnUpdated = DateTime.Now;
            context.SaveChanges();
        }
        public string OrderPortNumber(OrderPortNumber orderPortNumber)
        {
            var json = JsonConvert.SerializeObject(orderPortNumber);

            string outValue = "";
            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("ConnStr")))
            {
                SqlCommand cmd = new SqlCommand("sp_ForOrder", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@JSON_TEXT", json);

                SqlParameter output = new SqlParameter();
                output.ParameterName = "@RES";
                output.SqlDbType = System.Data.SqlDbType.VarChar;
                output.Size = 100;
                output.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(output);
                con.Open();
                cmd.ExecuteNonQuery();
                outValue = output.Value.ToString();
                con.Close();

                return outValue;
            }
        }

        public IEnumerable<OrderModel> GetAllOrderedPortNumber()
        {
            var listOfSimCardOrder = new List<OrderModel>();

            listOfSimCardOrder = context.Order.Where(x => x.IsDeleted == false &&
                                                        x.PortNumberId != null)
                .Select(x => new OrderModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    AlternateContactNo = x.AlternateContactNo,
                    SIMtypeName = x.Simtype.Name,
                    SIMtypeId = x.SimtypeId,
                    PortNumberId = x.PortNumberId,
                    PortNumber = x.PortNumber.Number,
                    Quantity = x.Quantity,
                    TotalPrice = x.TotalPrice,
                    AddessId = x.AddessId,
                    Status = ((OrderStatus)x.Status).ToString()
                }).ToList();

            return listOfSimCardOrder;
        }

        public OrderModel GetOrderedPortNumberById(int id)
        {
            var portNumberOrder = new OrderModel();

            portNumberOrder = context.Order.Where(x => x.IsDeleted == false &&
                                                        x.PortNumberId != null)
                .Select(x => new OrderModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    AlternateContactNo = x.AlternateContactNo,
                    SIMtypeName = x.Simtype.Name,
                    SIMtypeId = x.SimtypeId,
                    PortNumberId = x.PortNumberId,
                    PortNumber = x.PortNumber.Number,
                    Quantity = x.Quantity,
                    TotalPrice = x.TotalPrice,
                    AddessId = x.AddessId,
                    Status = ((OrderStatus)x.Status).ToString()
                }).FirstOrDefault();

            return portNumberOrder;
        }

        public string CheckPortNumberExist(string number)
        {
            var details = context.PortNumber.Where(x => x.Number == number && x.IsDeleted == false)
                                            .Select(x => new { PortNumber = x.Number })
                                            .FirstOrDefault();
            if (details != null)
                return details.PortNumber;
            else
                return null;
        }
    }
}
