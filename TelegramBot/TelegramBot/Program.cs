using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using CommandDLL;

namespace TelegramBot
{
    class Program
    {
        private static string token = "5398021083:AAEp9mJHuEwwZAvbSLsDYh8H6YlKBTTmZlo";
        private static TelegramBotClient client;
        private static Dictionary<long, ITelegramEventHandler> clients;
        private static ApiShell shell;

        public static ApiShell Shell { get => shell;}

        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);
            clients = new Dictionary<long, ITelegramEventHandler>();

            Console.WriteLine("Запущен бот " + client.GetMeAsync().Result.FirstName); //проверка запущен ли бот

            shell = new ApiShell();

            var profile = new WebApi.DTO.UserProfile()
            {
                Login = "BOT",
                Password = "12345",
                IsBot = true
            };

            shell.TrySignInAsync(profile).Wait();


            //var data = shell.GetSessionReportAsync(130).Result;

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token; //объект для остановки потоков
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // реакция на все типы updates 
            };
            client.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update)); //данные в консоли(удоли)
            Console.WriteLine();
            try
            {
               
                var chatID = update.Message.Chat.Id;
                var text = "Неизвестная команда: " + update.Message.Text;
                var clientId = update.Message.From.Id;
                     

                if (clients.ContainsKey(clientId))
                {
                    await clients[clientId].Handle(botClient, update);
                    return;

                }
                await ConstEvents.events[update.Message.Text].Handle(botClient, update);
            }
            catch (Exception e)
            {
                await ConstEvents.events["Неизвестная команда"].Handle(botClient, update);
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        public static void AddClientHandler(long id, ITelegramEventHandler handler) 
        {
            clients.Add(id, handler);
        }

        public static void RemoveClientHandler(long id) 
        {
            clients.Remove(id);
        }
    }

    static class ConstNameButton
    {
        public static readonly Dictionary<string, List<string>> names = new Dictionary<string, List<string>>();

        static ConstNameButton()
        {
            names.Add("Информация по договору", new List<string> { "Общее время работы с разбивкой по ионам", "Время начала работ по договору" });
            names.Add("Информация по иону", new List<string> { "Тип, энергия, пробег в кремнии", "Выработанное время на ионе по каждому договору", "Время затраченное на технологические перерывы и простои" });
            names.Add("Информация по сеансу", new List<string> { "Cтатус сеанса", "Время начала данного сеанса", "№ договора" });
            names.Add("Текущее состояние", new List<string> { "№ последнего сеанса и его статус", "Время начала последнего сеанса", "№ договора последнего сеанса" });
        }
    }

    static class ConstEvents
    {
        public static readonly Dictionary<string, ITelegramEventHandler> events = new Dictionary<string, ITelegramEventHandler>();

        static ConstEvents()
        {
            events.Add("/start", new TelegramEventButtons(ConstNameButton.names.Keys, "Добро пожаловать! Выполнено: "));

            events.Add("Информация по договору", new TelegramEventButtons(ConstNameButton.names["Информация по договору"]));
            events.Add("Информация по иону", new TelegramEventButtons(ConstNameButton.names["Информация по иону"]));
            events.Add("Информация по сеансу", new TelegramEventButtons(ConstNameButton.names["Информация по сеансу"]));
            events.Add("Текущее состояние", new TelegramEventButtons(ConstNameButton.names["Текущее состояние"]));

            events.Add("Неизвестная команда", new TelegramEventButtons(ConstNameButton.names.Keys, "Неизвестная команда: "));

            events.Add("Общее время работы с разбивкой по ионам",
                new TelegramEventAPI<Task<IEnumerable<TotalIonTimeUsing>>>(new TelegramEventButtons(),
                () => Program.Shell.GetIonTotalTimeUsingAsync(),
                (x) =>
                {
                    string result = "";
                    foreach (var item in x.Result)
                    {
                        result += item.IonName + " " + item.TotalTime + "\n";
                    }
                    return result;
                }));
            events.Add("Время начала работ по договору",
                new TelegramEventAPI<Task<IEnumerable<WebApi.DTO.ContractBegin>>>(new TelegramEventButtons(),
                () => Program.Shell.GetContractsBeginsAsync(),
                (x) =>
                {
                    string result = "";
                    foreach (var item in x.Result)
                    {
                        result += item.CompanyName + " " + item.WorkBegin + "\n";
                    }
                    return result;
                }));

            //events.Add("№ последнего сеанса и его статус", new TelegramEventAPI<Task<IEnumerable<TotalIonTimeUsing>>>(new TelegramEventButtons(), () => Program.Shell));
            //events.Add("Время начала последнего сеанса", new TelegramEventAPI<Task<IEnumerable<TotalIonTimeUsing>>>(new TelegramEventButtons()));
            //events.Add("№ договора последнего сеанса", new TelegramEventAPI<Task<IEnumerable<TotalIonTimeUsing>>>(new TelegramEventButtons()));

            events.Add("Тип, энергия, пробег в кремнии", new TelegramEventGetDopInfo("Напишите ион", new TelegramEventButtons()));
            events.Add("Выработанное время на ионе по каждому договору", new TelegramEventGetDopInfo("Напишите ион", new TelegramEventButtons()));
            events.Add("Время затраченное на технологические перерывы и простои", 
                new TelegramEventAPI<Task<TimeSpan>>(new TelegramEventButtons(), 
                () => Program.Shell.GetTotalTbAsync(),
                (x) => {return x.Result.ToString();}));

            events.Add("Cтатус сеанса", new TelegramEventGetDopInfo("Напишите номер сеанса", new TelegramEventButtons()));
            events.Add("Время начала данного сеанса", new TelegramEventGetDopInfo("Напишите номер сеанса", new TelegramEventButtons()));
            events.Add("№ договора", new TelegramEventGetDopInfo("Напишите номер сеанса", new TelegramEventButtons()));
        }
    }

    public interface ITelegramEventHandler
    {
        public Task Handle(ITelegramBotClient botClient, Update update);
    }

    class TelegramEventButtons : ITelegramEventHandler
    {
        private IEnumerable<string> names;
        private string textMessage;
        public TelegramEventButtons(IEnumerable<string> names, string textMessage = "Выполнено: ")
        {
            this.names = names;
            this.textMessage = textMessage;
        }

        public TelegramEventButtons()
        { }

        public Task Handle(ITelegramBotClient botClient, Update update)
        {
            var chatID = update.Message.Chat.Id;
            var text = textMessage + update.Message.Text;
            return botClient.SendTextMessageAsync(chatID, text, replyMarkup: GetButtons(names));
        }

        public IReplyMarkup GetButtons(IEnumerable<string> names)
        {
            var keyboard = new List<List<KeyboardButton>>();

            foreach (var item in names)
            {
                var keyboardButtons = new List<KeyboardButton>();
                keyboard.Add(keyboardButtons);
                keyboardButtons.Add(new KeyboardButton(item));
            }

            return new ReplyKeyboardMarkup(keyboard);
        }
    }

    class TelegramEventAPI<T> : ITelegramEventHandler
    {
        private TelegramEventButtons eventButtons;
        private Func<T> apiFunc;
        private Func<T, string> resultFormater;
        public TelegramEventAPI(TelegramEventButtons eventButtons, Func<T> apiFunction, Func<T, string> resultFormater)
        {
            this.eventButtons = eventButtons;
            this.resultFormater = resultFormater;
            apiFunc = apiFunction;
        }

        public Task Handle(ITelegramBotClient botClient, Update update)
        {
            var chatID = update.Message.Chat.Id;
            var text = update.Message.Text;
            var result = resultFormater(apiFunc());

            return botClient.SendTextMessageAsync(chatID, result, replyMarkup: eventButtons.GetButtons(ConstNameButton.names.Keys));
        }

        private string GetInfo()
        {
            return "Информация по запросу: " + apiFunc();
        }
    }

    class TelegramEventGetDopInfo : ITelegramEventHandler
    {
        private string message;
        private TelegramEventButtons eventButtons;
        public TelegramEventGetDopInfo(string message, TelegramEventButtons eventButtons)
        {
            this.message = message;
            this.eventButtons = eventButtons;
        }
        public Task Handle(ITelegramBotClient botClient, Update update)
        {
            Program.AddClientHandler(update.Message.From.Id, new TelegramEventReturnInfo(update.Message.Text, eventButtons));
            var chatID = update.Message.Chat.Id;
            //var text = update.Message.Text;

            return botClient.SendTextMessageAsync(chatID, message, replyMarkup: new ReplyKeyboardRemove());
        }
    }

    class TelegramEventReturnInfo : ITelegramEventHandler
    {
        private string nameCommand;
        private TelegramEventButtons eventButtons;
        public TelegramEventReturnInfo(string nameCommand, TelegramEventButtons eventButtons)
        {
            this.nameCommand = nameCommand;
            this.eventButtons = eventButtons;
        }
        public Task Handle(ITelegramBotClient botClient, Update update)
        {
            var chatID = update.Message.Chat.Id;
            var text = update.Message.Text;

            Program.RemoveClientHandler(update.Message.From.Id);
            return botClient.SendTextMessageAsync(chatID, GetInfo(nameCommand, text), replyMarkup: eventButtons.GetButtons(ConstNameButton.names.Keys));
        }

        private string GetInfo(string command, string dopInfo)
        {
            return "Информация по запросу: " + command + " " + dopInfo;
        }
    }
}
