using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meetup.Core;
using Meetup.Core.Services;


namespace _11_MeetupReminder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the name of the group you want to see meetups for");
            string groupName = Console.ReadLine();

            List<MeetupEvent> meetups = MeetupService.GetMeetupsFor(groupName).Result;

            string meetupMessage = "";

            foreach (var meetup in meetups)
            {   
                meetupMessage += "\n" + meetup.name + " \n";
                meetupMessage += meetup.event_url + "\n";
            }

            TwillioService.Text(meetupMessage);
        }
    }
}
