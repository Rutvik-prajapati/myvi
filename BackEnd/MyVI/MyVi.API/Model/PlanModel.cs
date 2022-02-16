using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Model
{
    public class PlanModel
    {
        public int Id { get; set; }
        public int PlanTypeId { get; set; }
        public double Talktime { get; set; }
        public int Price { get; set; }
        public string Call { get; set; }
        public string Data { get; set; }
        public string Sms { get; set; }
        public int Connection { get; set; }
        public int Validity { get; set; }
        public string Benefits { get; set; }
    }
}
