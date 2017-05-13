using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;
using System.ServiceModel.Channels;
using System.IO;
using Twilio;

namespace FasciaAlertWebJob
{
    class Program
    {
        static void Main()
        {
            var host = new JobHost();
            host.CallAsync(typeof(Functions).GetMethod("ProcessMethod"));
            host.RunAndBlock();
        }
    }
}
