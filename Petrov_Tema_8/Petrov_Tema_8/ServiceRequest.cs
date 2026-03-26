using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class ServiceRequest
    {
        public int Id {  get; set; }
        public string CustomerName { get; set; }
        public string RequestType {  get; set; }
        public ServiceRequest(int id, string customerName, string requestType)
        {
            Id = id;
            CustomerName = customerName;
            RequestType = requestType;
        }

    }
}
