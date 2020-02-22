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


        public void Put(HttpRequestMessage value)
        {
            //Console.WriteLine(value.Content.ReadAsStringAsync().Result);
            Program.theInboundMessageQueue.Send(value.Content.ReadAsStringAsync().Result);
        }


    }
}
