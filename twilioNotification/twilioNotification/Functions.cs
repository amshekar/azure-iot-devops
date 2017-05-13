using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Twilio;
using System.Configuration;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus.Notifications;
using Microsoft.WindowsAzure;

namespace twilioNotification
{
    public class Functions
    {

        public static void ProcessTopic([ServiceBusTrigger("fasciaalerttopic", "Webjob")] string message,
       TextWriter logger)
        {
            logger.WriteLine("Topic message: " + message);

            try
            {

                var twilioClient = new TwilioRestClient(ConfigurationManager.AppSettings["ACCOUNTSID"], ConfigurationManager.AppSettings["AUTHTOKEN"]);
                bool SMSAlert = Convert.ToBoolean(CloudConfigurationManager.GetSetting("SMSAlert"));
                bool PushNotification = Convert.ToBoolean(CloudConfigurationManager.GetSetting("PushNotification"));
                string NotificationHubConnectionString = CloudConfigurationManager.GetSetting("NotificationHubConnectionString");
                string NotificationHubName = "fasciahub";
                
                var toList = ConfigurationManager.AppSettings["USERMOBILES"];

                if (SMSAlert)
                {
                    foreach (var to in toList.Split(','))
                    {
                        var result = twilioClient.SendMessage(ConfigurationManager.AppSettings["FROM"], to, message);
                    }
                }

                if (PushNotification)
                {
                    NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString(NotificationHubConnectionString, NotificationHubName);

                    //dynamic data = new JsonReader().Read(incommingMessage);
                    //string publishMessage = string.Empty;

                    //foreach (var item in data)
                    //{
                    //    if (item.Key == "message")
                    //        publishMessage = item.Value;
                    //}

                    String messageToClient = string.Concat("{\"data\":{\"Message\":\"", message, "\"}}");
                    hub.SendGcmNativeNotificationAsync(messageToClient);
                }
            }
            catch (Exception ex)
            {

                logger.WriteLine(ex.Message);
            }
        }
       
    }
}
