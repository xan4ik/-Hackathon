using CommandDLL;
using Domain.DTO;
using System;

namespace TestCommandDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            var profile = new UserProfile()
            {
                Login = "BOT",
                Password = "12345",
                IsBot = true
            };

            using (var shell = new ApiShell())
            {
                var builder = new NonStandartPDFProtocolBuilder();

                shell.TrySignInAsync(profile).Wait();
                var result = shell.GetNonStandartDocumentData(43).Result;

                builder.LoadTemplate(@"C:\Users\Lev\source\repos\-Hackathon\CommandDLL\html\non_standard.html");
                builder.SetContent(result);
                builder.SetDocumentName("ass2.pdf");
                var documet = builder.CreateDocument();

                documet.SaveAsync(".").Wait();

                shell.TrySignOutAsync().Wait();
            }




            Console.WriteLine();

            //var bulder = new NonStandardPDFProtocolBuilder();
            //bulder.LoadTemplate(@"C:\Users\Lev\source\repos\-Hackathon\CommandDLL\html\allowance.html");
            //bulder.SetDocumentName("test.pdf");
            //var document = bulder.CreateDocument();

            //document.Save(".");

            //EmailSender sender = new EmailSender();
            //sender.SendDocumentAsync("stack@uni-dubna.ru", document).Wait();


            Console.WriteLine("Hello World!");
        }
    }
}
