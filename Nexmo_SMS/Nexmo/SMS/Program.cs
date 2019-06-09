using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexmo;
using Nexmo.Api;

namespace SMS
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Nexmo.Api.Client(creds: new Nexmo.Api.Request.Credentials { ApiKey = "a13c5956", ApiSecret = "It62MH4bBVU9k7BZ" });
            var result = client.SMS.Send(request: new Nexmo.Api.SMS.SMSRequest { from = "NexmoDemo", to = "6584531029", text = "Send Chinese to Melody's Phone: 中文测试", type = "unicode" });
        }
    }
}
