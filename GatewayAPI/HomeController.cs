using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Messaging;
using System.Net.Http;

namespace GatewayAPI
{
    public class HomeController : ApiController
    {
        public string Get()
        {
            return "Running";
        }


        public string Put(HttpRequestMessage value)
        {
            Console.WriteLine(value.Content.ReadAsStringAsync().Result);
            MessageQueue myQueue = new MessageQueue(Program.theInboundPath);
            myQueue.Send(value.Content.ReadAsStringAsync().Result);
            return value.Content.ReadAsStringAsync().Result;
        }


    }
}
