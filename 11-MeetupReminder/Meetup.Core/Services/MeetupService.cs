using CSharp.Meetup.Connect;
using Spring.Social.OAuth1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;


namespace Meetup.Core.Services
{
    

    public class MeetupService
    {
        private const string MeetupApiKey = "stbspbgc5ogpti306nf2gb1569";
        private const string MeetupSecretKey = "cgj6va980p17v7u40u16s9c35m";

        private static async Task<OAuthToken> Authenticate() 
        {
            var meetupServiceProvider = new MeetupServiceProvider(MeetupApiKey, MeetupSecretKey);

            /* OAuth 'dance' */
            Console.Write("Getting request token...");
            var oauthToken = await meetupServiceProvider.OAuthOperations.FetchRequestTokenAsync("oob", null);
            Console.WriteLine("Done");

            var authenticateUrl = meetupServiceProvider.OAuthOperations.BuildAuthorizeUrl(oauthToken.Value, null);
            Console.WriteLine("Redirect user for authentication: " + authenticateUrl);
            Process.Start(authenticateUrl);
            Console.WriteLine("Enter PIN Code from Meetup authorization page:");
            var pinCode = Console.ReadLine();

            Console.Write("Getting access token...");
            var requestToken = new AuthorizedRequestToken(oauthToken, pinCode);
            var oauthAccessToken = meetupServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null).Result;
            Console.WriteLine("Done");

            return oauthAccessToken;
        }

        public static async Task<List<MeetupEvent>> GetMeetupsFor(string groupName)
        {
            //Create a list of MeetUpevents

            var token = await Authenticate();

            var meetupServiceProvider = new MeetupServiceProvider(MeetupApiKey, MeetupSecretKey);
            var meetup = meetupServiceProvider.GetApi(token.Value, token.Secret);

            string json = await meetup.RestOperations.GetForObjectAsync<string>($"https://api.meetup.com/2/events?group_urlname={groupName}");
            var o = JObject.Parse(json);
            string resultsJson = o["results"].ToString();
            List<MeetupEvent> meetups = JsonConvert.DeserializeObject<List<MeetupEvent>>(resultsJson);

            return meetups;

        }
    }
}
