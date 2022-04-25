using Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.DTO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var invoker = new ApiShell();

            var profile = new UserProfile()
            {
                Login = "Admin",
                Password = "12345",
                IsBot = false
            };


            invoker.TrySignInAsync(profile).Wait();

            var res = invoker.GetIonTotalTimeUsingAsync().Result.ToArray();

            //var res = invoker.GetContractsBegins().Result.ToArray();
            Console.WriteLine();
            //var result1 = invoker.GetIonTotalTimeUsingAsync().Result;
            //foreach (var item in result1)
            //{
            //    Console.WriteLine(item.IonName + " " + item.TotalTime);
            //}

            //Console.WriteLine();

            //var result2 = invoker.GetIonShortInfoAsync().Result;
            //foreach (var item in result2)
            //{
            //    Console.WriteLine(item.IonName + " " + item.Isotope);
            //}


            //invoker.TrySignOutAsync().Wait();
            Console.WriteLine();
         }

    }
}




