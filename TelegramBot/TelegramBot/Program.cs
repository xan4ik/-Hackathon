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
using Domain.DTO;
using CommandDLL.DTO;

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

            var profile = new UserProfile()
            {
                Login = "BOT",
                Password = "12345",
                IsBot = true
            };

            shell.TrySignInAsync(profile).Wait();


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
                //var text = "Неизвестная команда: " + update.Message.Text;
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
            events.Add("/start", new TelegramEventButtons(ConstNameButton.names.Keys, "Добро пожаловать! Выполнено: ")); //+

            events.Add("Информация по договору", new TelegramEventButtons(ConstNameButton.names["Информация по договору"])); //+
            events.Add("Информация по иону", new TelegramEventButtons(ConstNameButton.names["Информация по иону"])); //+
            events.Add("Информация по сеансу", new TelegramEventButtons(ConstNameButton.names["Информация по сеансу"])); //+
            events.Add("Текущее состояние", new TelegramEventButtons(ConstNameButton.names["Текущее состояние"])); //+

            events.Add("Неизвестная команда", new TelegramEventButtons(ConstNameButton.names.Keys, "Неизвестная команда: ")); //+

            events.Add("Общее время работы с разбивкой по ионам", //+
                new TelegramEventAPI<Task<IEnumerable<TotalIonTimeUsing>>>(new TelegramEventButtons(),
                () => Program.Shell.GetIonTotalTimeUsingAsync(),
                (x) =>
                {
                    string result = "";
                    foreach (var item in x.Result)
                    {
                        result += item.IonName + " - Количество дней: " + item.TotalTime.Days + ". Часы: " + item.TotalTime.Hours + ":" 
                        + item.TotalTime.Minutes + ":" + item.TotalTime.Seconds + "\n";
                    }
                    return result;
                }));
            events.Add("Время начала работ по договору", //+
                new TelegramEventAPI<Task<IEnumerable<ContractBegin>>>(new TelegramEventButtons(),
                () => Program.Shell.GetContractsBeginsAsync(),
                (x) =>
                {
                    string result = "";
                    foreach (var item in x.Result)
                    {
                        result += item.CompanyName + " \n\t- Дата: " + item.WorkBegin.ToShortDateString() + ". Время: " + 
                        item.WorkBegin.ToShortTimeString() + "\n";
                    }
                    return result;
                }));

            events.Add("№ последнего сеанса и его статус", new TelegramEventAPI<Task<SessionReport>>(new TelegramEventButtons(), //+
                () => Program.Shell.GetSessionReportAsync(Program.Shell.GetSessionCountAsync().Result),
                (x) => 
                {
                    var report = x.Result;
                    return "№ последнего сеанса: " + report.SessionNumber+ ". Статус: " + report.Status;
                }
                ));
            events.Add("Время начала последнего сеанса", new TelegramEventAPI<Task<DateTime>>(new TelegramEventButtons(), //+
                () => Program.Shell.GetSessionBeginAsync(Program.Shell.GetSessionCountAsync().Result),
                (x) =>
                {
                    var report = x.Result;
                    return "Дата: " + report.ToShortDateString() + ". Время: " + report.ToShortTimeString();
                }
                ));
            events.Add("№ договора последнего сеанса", new TelegramEventAPI<string>(new TelegramEventButtons(), //+
                () => "xx-xxxx", (x) => x));

            events.Add("Тип, энергия, пробег в кремнии", new TelegramEventGetDopInfo<IonShortInfo>("Напишите ион", 
                new TelegramEventButtons(),
                new TelegramEventReturnInfo<IonShortInfo>( new TelegramEventButtons(),
                    (update) => Program.Shell.GetIonShortInfoAsync(update.Message.Text).Result,
                    (x) => "Тип: " + x.Isotope + ", пробег в кремнии" + x.DistanceSI //TODO //добавить энергию, СТАС!!! 
                    )));

            events.Add("Выработанное время на ионе по каждому договору", new TelegramEventGetDopInfo<IEnumerable<ContractTimeWorkByIon>>(
                "Напишите ион", 
                new TelegramEventButtons(),
                new TelegramEventReturnInfo<IEnumerable<ContractTimeWorkByIon>>(new TelegramEventButtons(),
                    (update) => Program.Shell.GetContractsTimeworkAsync(update.Message.Text).Result,
                    (x) =>
                    {
                        string result = "";
                        foreach (var item in x)
                        {
                            result += item.CompanyName + " \n\t- Количество дней: " + item.TotalTimeSpan.Days + ". Часы: " 
                            + item.TotalTimeSpan.Hours + ":" + item.TotalTimeSpan.Minutes + ":" + item.TotalTimeSpan.Seconds + "\n";
                        }
                        return result;
                    }     
                    )));

            events.Add("Время затраченное на технологические перерывы и простои", //+
                new TelegramEventAPI<Task<TimeSpan>>(new TelegramEventButtons(),
                () => Program.Shell.GetTotalTbAsync(),
                (x) => {return x.Result.ToString();}));

            events.Add("Cтатус сеанса", new TelegramEventGetDopInfo<SessionReport>( //+
                    "Напишите номер сеанса",
                    new TelegramEventButtons(),
                    new TelegramEventReturnInfo<SessionReport>(new TelegramEventButtons(),
                        (update) => Program.Shell.GetSessionReportAsync(int.Parse(update.Message.Text)).Result,
                        (x) => x.Status
                    )));

            events.Add("Время начала данного сеанса", new TelegramEventGetDopInfo<DateTime>( //+
                "Напишите номер сеанса", 
                new TelegramEventButtons(),
                new TelegramEventReturnInfo<DateTime>( new TelegramEventButtons(),
                    (update) => Program.Shell.GetSessionBeginAsync(int.Parse(update.Message.Text)).Result,
                    (x) => "Дата: " + x.ToShortDateString() + ". Время: " + x.ToShortTimeString()                   
                )));

            events.Add("№ договора", new TelegramEventGetDopInfo<string>("Напишите номер сеанса", new TelegramEventButtons(), //+
                new TelegramEventReturnInfo<string>(new TelegramEventButtons(),
                (update) => "xx-xxxx", 
                (x) => x)));
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
    }

    class TelegramEventGetDopInfo<T> : ITelegramEventHandler
    {
        private string message;
        private TelegramEventButtons eventButtons;
        private TelegramEventReturnInfo<T> eventReturnInfo;

        public TelegramEventGetDopInfo(string message, TelegramEventButtons eventButtons, TelegramEventReturnInfo<T> eventReturnInfo) 
        {
            this.message = message;
            this.eventButtons = eventButtons;
            this.eventReturnInfo = eventReturnInfo;
        }
        public Task Handle(ITelegramBotClient botClient, Update update)
        {
            Program.AddClientHandler(update.Message.From.Id, eventReturnInfo);
            var chatID = update.Message.Chat.Id;

            return botClient.SendTextMessageAsync(chatID, message, replyMarkup: new ReplyKeyboardRemove());
        }
    }

    class TelegramEventReturnInfo<T> : ITelegramEventHandler
    {
        private TelegramEventButtons eventButtons;
        private Func<Update, T> apiFunc;
        private Func<T, string> resultFormater;
        public TelegramEventReturnInfo(TelegramEventButtons eventButtons, Func<Update, T> apiFunc, Func<T, string> resultFormater)
        {
            this.eventButtons = eventButtons;
            this.apiFunc = apiFunc;
            this.resultFormater = resultFormater;
        }
        public Task Handle(ITelegramBotClient botClient, Update update)
        {
            var chatID = update.Message.Chat.Id;
            var text = update.Message.Text;
            Program.RemoveClientHandler(update.Message.From.Id);

            var result = resultFormater(apiFunc(update));
            return botClient.SendTextMessageAsync(chatID, result, replyMarkup: eventButtons.GetButtons(ConstNameButton.names.Keys));
        }
    }
}
