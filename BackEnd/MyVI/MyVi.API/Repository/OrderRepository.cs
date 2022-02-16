using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyVi.API.Entities;
using MyVi.API.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrder
    {
        private IConfiguration configuration;
        public OrderRepository(MyVIDBContext context, IConfiguration _configuration) : base(context)
        {
            configuration = _configuration;
        }
        public string NewOrder<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);

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
    }
}
