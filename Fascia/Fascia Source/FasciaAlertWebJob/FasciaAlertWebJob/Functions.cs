using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;

namespace FasciaAlertWebJob
{
    public class Functions
    {

        [NoAutomaticTriggerAttribute]
        public static async Task ProcessMethod(TextWriter log)
        {
            //Publish messages
            AlertTopic.ReceiveMessages(); 
        }
    }
}
