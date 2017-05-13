using Microsoft.Azure;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMessageToQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Endpoint=sb://fasciaeventhub.servicebus.windows.net/;SharedAccessKeyName=FullAccess;SharedAccessKey=MkqJUno+Wdox4c/2kriTgXrb7fihmaLIEOekiuAWQBs=";
            bool SMSAlert = Convert.ToBoolean(CloudConfigurationManager.GetSetting("SMSAlert"));
            bool PushNotification = Convert.ToBoolean(CloudConfigurationManager.GetSetting("PushNotification"));


            TopicClient Client =
                TopicClient.CreateFromConnectionString(connectionString, "fasciaalerttopic");

            for (int i = 0; i < 2; i++)
            {
                // Create message, passing a string message for the body.
                BrokeredMessage message = new BrokeredMessage("Test message " + i);

                // Set some addtional custom app-specific properties.
                message.Properties["TestProperty"] = "TestValue";
                message.Properties["Message number"] = i;

                // Send message to the queue.
                Client.Send(message);
            }

           
            //// Configure the callback options.
            //OnMessageOptions options = new OnMessageOptions();
            //options.AutoComplete = false;
            //options.AutoRenewTimeout = TimeSpan.FromMinutes(1);

            //// Callback to handle received messages.
            //Client.OnMessage((message) =>
            //{
            //    try
            //    {
            //        // Process message from queue.
            //        Console.WriteLine("Body: " + message.GetBody<string>());
            //        Console.WriteLine("MessageID: " + message.MessageId);
            //        Console.WriteLine("Test Property: " +
            //        message.Properties["TestProperty"]);

            //        // Remove message from queue.
            //        message.Complete();
            //    }
            //    catch (Exception)
            //    {
            //        // Indicates a problem, unlock message in queue.
            //        message.Abandon();
            //    }
            //}, options);

            //Console.Read();
        }
    }
}
