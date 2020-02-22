using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Messaging;


namespace GatewayAPI
{
    public class HomeController : ApiController
    {
        public string Get()
        {
            return "Running";
        }


        public string Put(string raw)
        {
                //MessageQueue myQueue = new MessageQueue(".\\private$\\IOTData");
                MessageQueue myQueue = new MessageQueue(Program.theNotificationsPath);
                myQueue.Send(raw);
                return "Put Done";
        }
    }
}
