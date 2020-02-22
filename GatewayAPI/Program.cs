using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Messaging;

namespace GatewayAPI
{
    class Program
    {
        // Constants for working with MSMQ
        public static string theInboundPath = @".\Private$\IOTData";
        public static string theNotificationsPath = @".\Private$\IOTNotifications";
        public static MessageQueue theInboundMessageQueue = new MessageQueue(theInboundPath);
        public static MessageQueue theNotificationsMessageQueue = new MessageQueue(theNotificationsPath);

        static void Main(string[] args)
        {
            // IOT Inbound Path 
            try
            {
                //If message queues do not exist, create them
                MessageQueue myMessageQueue = null;
                string myInboundDescription = "IOT Inbound Queue";
                string myInboundPath = theInboundPath;


                if (MessageQueue.Exists(myInboundPath))
                {
                    myMessageQueue = new MessageQueue(myInboundPath);
                    myMessageQueue.Label = myInboundDescription;
                }
                else
                {
                    MessageQueue.Create(myInboundPath);
                    myMessageQueue = new MessageQueue(myInboundPath);
                    myMessageQueue.Label = myInboundDescription;
                }
            }
            catch (Exception myException)
            {
                Console.WriteLine("Error:: " + myException.Message);
            }

            //IOT Notification Path
            try
            {
                MessageQueue myMessageQueue = null;
                string myNotificationDescription = "IOT Notification Queue";
                string myNotificationsPath = theNotificationsPath;

                if (MessageQueue.Exists(myNotificationsPath))
                {
                    myMessageQueue = new MessageQueue(myNotificationsPath);
                    myMessageQueue.Label = myNotificationDescription;
                }
                else
                {
                    MessageQueue.Create(myNotificationsPath);
                    myMessageQueue = new MessageQueue(myNotificationsPath);
                    myMessageQueue.Label = myNotificationDescription;
                }
            }
            catch (Exception myException)
            {
                Console.WriteLine("Error:: " + myException.Message);
            }


            //Start listener
            try
            {
                var config = new HttpSelfHostConfiguration("http://localhost:5801");
                config.Routes.MapHttpRoute("default",
                                            "api/537/{controller}/{id}",
                                            new { controller = "Home", id = RouteParameter.Optional });

                var server = new HttpSelfHostServer(config);
                var task = server.OpenAsync();
                task.Wait();

                Console.WriteLine("Web API Server has started at http://localhost:5801");
                Console.ReadLine();
            }
            catch (Exception myException)
            {
                Console.WriteLine("Error:: " + myException.Message);
            }
        }
    }
    
}
