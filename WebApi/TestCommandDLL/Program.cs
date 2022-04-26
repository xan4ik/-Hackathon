using CommandDLL;
using Domain.DTO;
using System;

namespace TestCommandDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            //var profile = new UserProfile()
            //{
            //    Login = "BOT",
            //    Password = "12345",
            //    IsBot = true
            //};

            //using (var shell = new ApiShell()) 
            //{
            //    shell.TrySignInAsync(profile).Wait();
            //    var result1 = shell.GetSessionCountAsync().Result;

            //    var result2 = shell.GetSessionReportAsync(result1).Result;

            //    shell.TrySignOutAsync().Wait();
            //    Console.ReadLine();
            //}

            var bulder = new NonStandardPDFProtocolBuilder();
            bulder.LoadTemplate(@"C:\Users\Lev\source\repos\-Hackathon\CommandDLL\html\allowance.html");
            bulder.SetDocumentName("test.pdf");
            var document = bulder.CreateDocument();

            document.Save(".");

            //EmailSender sender = new EmailSender();
            //sender.SendDocumentAsync("stack@uni-dubna.ru", document).Wait();
            

            Console.WriteLine("Hello World!");
        }
    }
}
