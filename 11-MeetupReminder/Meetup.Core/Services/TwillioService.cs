using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Model;

namespace Meetup.Core.Services
{
    public class TwillioService
    {
        public static void Call (string textMessage)
        {
            // Find your Account Sid and Auth Token at twilio.com/user/account 
            string AccountSid = "AC0b7c6dbd76844d6b1a965666690eeaca";
            string AuthToken = "070d50995710e42c016602ed3ec5825c";
                var twilio = new TwilioRestClient(AccountSid, AuthToken);
            

            var message = twilio.SendMessage("+18475652490", " +16302207607", textMessage);
            Console.WriteLine(message.Sid);
        }

    }
}
