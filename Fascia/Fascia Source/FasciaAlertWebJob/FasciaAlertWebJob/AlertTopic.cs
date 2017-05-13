
using JsonFx.Json;
using Microsoft.Azure;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus.Notifications;
using Microsoft.WindowsAzure;
using System;

using Twilio;

namespace FasciaAlertWebJob
{
    public class AlertTopic
    {
        //Sending notification to GCM (Google Chrome Applicaiton)
        private static async void PushGCMNotificationAsync(string incommingMessage)
        {
            try
            {
                string NotificationHubConnectionString = CloudConfigurationManager.GetSetting("NotificationHubConnectionString");
                string NotificationHubName = CloudConfigurationManager.GetSetting("NotificationHubName");
                NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString(NotificationHubConnectionString, NotificationHubName);

                dynamic data = new JsonReader().Read(incommingMessage);
                string publishMessage = string.Empty;

                foreach (var item in data)
                {
                    if (item.Key == "message")
                        publishMessage = item.Value;
                }

                String message = string.Concat("{\"data\":{\"Message\":\"", publishMessage, "\"}}");
                await hub.SendGcmNativeNotificationAsync(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        //Sending notification SMS using Twilio
        private static void SendTwilioSMSNotification(string incommingMessage)
        {
            if (incommingMessage != null)
            {
                string personId = string.Empty;
                string informationMessage = string.Empty;
                string mobileId1 = CloudConfigurationManager.GetSetting("DefaultSMSNumber");
                string mobileId2 = string.Empty;

                dynamic data = new JsonReader().Read(incommingMessage);

                foreach (var item in data)
                {
                    if (item.Key == "sensorid")
                        personId = item.Value;
                    if (item.Key == "message")
                        informationMessage = item.Value;
                    if (item.Key == "contact1")
                        mobileId1 = item.Value.ToString();
                    if (item.Key == "contact2")
                        mobileId2 = item.Value.ToString();
                }

                string accountSID = CloudConfigurationManager.GetSetting("TwilioAccountSID");
                string authToken = CloudConfigurationManager.GetSetting("TwilioAuthToken");
                string fromSmsNumber = CloudConfigurationManager.GetSetting("TwilioSMSFrom");

                // Create an instance of the Twilio client.
                TwilioRestClient client;
                client = new TwilioRestClient(accountSID, authToken);
                TwilioRestClient client1;
                client1 = new TwilioRestClient(accountSID, authToken);

                // Send an SMS message.
                var result = client.SendMessage(
                fromSmsNumber, mobileId1, informationMessage);

                if (mobileId2 != string.Empty)
                {
                    var result1 = client1.SendMessage(
                    fromSmsNumber, mobileId2, informationMessage);
                }
            }
        }

        //Receive message from Fascia Topic
        public static void ReceiveMessages()
        {
            string TopicName = CloudConfigurationManager.GetSetting("AlertTopicName");
            string SubscriptionName = CloudConfigurationManager.GetSetting("SubscriptionName");
            bool SMSAlert = Convert.ToBoolean(CloudConfigurationManager.GetSetting("SMSAlert"));
            bool PushNotification = Convert.ToBoolean(CloudConfigurationManager.GetSetting("PushNotification"));
            BrokeredMessage message = null;

            // For ReceiveAndDelete mode, where applications require "best effort" delivery of messages
            SubscriptionClient alertSubscriptionClient = SubscriptionClient.Create(TopicName, SubscriptionName, ReceiveMode.ReceiveAndDelete);
            while (true)
            {
                try
                {
                    message = alertSubscriptionClient.Receive(TimeSpan.FromSeconds(5));
                    if (message != null)
                    {
                        string messageBody = message.GetBody<string>();
                        if(PushNotification)
                        PushGCMNotificationAsync(messageBody);
                        if(SMSAlert)
                        SendTwilioSMSNotification(messageBody);
                    }
                    else
                    {
                        break;
                    }
                }
                catch (MessagingException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            alertSubscriptionClient.Close();
        }
    }
}
