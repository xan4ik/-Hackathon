using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;

namespace TelegramBot
{
    class Program
    {
        private static string token = "5332022872:AAHDxLUT0ZRxH8KsjUlLtloE68WDAKgTiVM";
        private static TelegramBotClient client;

        private static CommandHandler root;
        private static CommandHandler lastActive;

        //private static CommandHandler handler;
        //private static Dictionary<string, CommandHandler> handlers;

        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);

            var handlers = new Dictionary<string, CommandHandler>();

            //var end = new UnknowenCommandHandler();

            var handler1 = new ApiCommandHandler("Общее время работы с разбивкой по ионам");
            var handler2 = new ApiCommandHandler("Время начала работ по договору", handler1);

            var handler3 = new GetButtonCommandHandler("Информация по договору", ConstNameButton.names["Информация по договору"], handler2);

            var handler4 = new ApiCommandHandler("Тип, энергия, пробег в кремнии");
            var handler5 = new ApiCommandHandler("Выработанное время на ионе по каждому договору", handler4);
            var handler6 = new ApiCommandHandler("Время затраченное на технологические перерывы и простои", handler5);

            var handler7 = new GetButtonCommandHandler("Информация по иону", ConstNameButton.names["Информация по иону"], handler6);

            var handler8 = new ApiCommandHandler("№ сеанса и его статус");
            var handler9 = new ApiCommandHandler("Время начала данного сеанса", handler8);
            var handler10 = new ApiCommandHandler("№ договора", handler9);

            var handler11 = new GetButtonCommandHandler("Текущее состояние", ConstNameButton.names["Текущее состояние"], handler10);

            handlers.Add("Информация по договору", handler3);
            handlers.Add("Информация по иону", handler7);
            handlers.Add("Текущее состояние", handler11);
            //handlers.Add("/start", )
            //handlers.Add("default", end);

            root = new BranchCommand(handlers);
            lastActive = root;

            Console.WriteLine("Запущен бот " + client.GetMeAsync().Result.FirstName); //проверка запущен ли бот

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
                lastActive = root.FindHandler(update.Message.Text);
                await lastActive.Handle(botClient, update);

            }
            catch (Exception e) 
            {
                lastActive = root;
                await new UnknowenCommandHandler().Handle(botClient, update);
            }
            //var message = update.Message;
            //var activeHandler = handlers["default"];

            //if (handlers.ContainsKey(message.Text))
            //{
            //    activeHandler = handlers[message.Text];
            //}
            //else if(SearchBool(message.Text))
            //{
            //    activeHandler = handlers[Search(message.Text)];
            //}

           // handler = new GetButtonCommandHandler("кнопки", ConstNameButton.names.Keys, activeHandler);
            //await handler.HandleCommand(botClient, update);
        }

        private static bool SearchBool(string message)
        {
            if(Search(message)!="")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string Search(string message)
        {
            foreach (var item in ConstNameButton.names)
            {
                foreach (var i in item.Value)
                {
                    if(i.Contains(message))
                    {
                        return item.Key;
                    }
                }
            }

            return "";
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


    }

    static class ConstNameButton
    {
        public static readonly Dictionary<string, List<string>> names = new Dictionary<string, List<string>>();

        static ConstNameButton()
        {
            names.Add("Информация по договору", new List<string> { "Общее время работы с разбивкой по ионам", "Время начала работ по договору" });
            names.Add("Информация по иону", new List<string> { "Тип, энергия, пробег в кремнии", "Выработанное время на ионе по каждому договору", "Время затраченное на технологические перерывы и простои" });
            names.Add("Текущее состояние", new List<string> { "№ сеанса и его статус", "Время начала данного сеанса", "№ договора" });
            names.Add("Добавить информацию", new List<string> { "Таблица <<Data>>", "Таблица <<Timing>>", "Таблица <<Информация по иону>>" });
        }
    }


    abstract class CommandHandler
    {
        private CommandHandler next;

        public CommandHandler(CommandHandler next)
        {
            this.next = next;
        }

        public CommandHandler()
        { }

        public CommandHandler FindHandler(string command)
        {
            if (CanHandle(command))
            {
                return GetHandler(command);
                //return OnHnadle(botClient, command);
            }
            else
            {
                if (next == null)
                {
                    throw new Exception("Unknown command");
                }
                return next.FindHandler(command);
            }
        }

        protected virtual CommandHandler GetHandler(string command)
        {
            return this;
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

        public abstract bool CanHandle(string command);
        public abstract Task Handle(ITelegramBotClient botClient, Update command);
    }

    
    class UnknowenCommandHandler : CommandHandler
    {
        public override bool CanHandle(string command)
        {
            return true;
        }

        public override Task Handle(ITelegramBotClient botClient, Update command)
        {
            var chatID = command.Message.Chat.Id;
            var messageText = "Не известна команда " + command.Message.Text;

            return botClient.SendTextMessageAsync(chatID, messageText, replyMarkup: GetButtons(ConstNameButton.names.Keys));
        }
    }

    class ApiCommandHandler : CommandHandler
    {
        private string command;
        public ApiCommandHandler(string command, CommandHandler next) : base(next)
        {
            this.command = command;
        }

        public ApiCommandHandler(string command) 
        {
            this.command = command;
        }

        public override bool CanHandle(string command)
        {
            return this.command == command;
        }

        public override Task Handle(ITelegramBotClient botClient, Update command)
        {
            var chatID = command.Message.Chat.Id;
            var messageText = "выполнено  " + command.Message.Text;

            return botClient.SendTextMessageAsync(chatID, messageText);
        }
    }



    class GetButtonCommandHandler : CommandHandler
    {
        private IEnumerable<string> result;
        private string command;

        public GetButtonCommandHandler(string command, IEnumerable<string> result, CommandHandler next) : base(next)
        {
            this.result = result;
            this.command = command;
        }

        public override bool CanHandle(string command)
        {
            return this.command == command;
        }

        public override Task Handle(ITelegramBotClient botClient, Update command)
        {
            var chatID = command.Message.Chat.Id;
            var messageText = "выполнено";

            return botClient.SendTextMessageAsync(chatID, messageText, replyMarkup: GetButtons(result));
        }
    }


    class BranchCommand : CommandHandler    
    {
        private Dictionary<string, CommandHandler> branches;

        public BranchCommand(Dictionary<string, CommandHandler> handlers)
        {
            branches = handlers;
        }

        public override bool CanHandle(string command)
        {
            foreach (var item in branches.Keys)
            {
                if(branches[item].CanHandle(command))
                {
                    return true;
                }
            }

            return false;
        }

        protected override CommandHandler GetHandler(string command)
        {
            return branches[command];
        }

        public override Task Handle(ITelegramBotClient botClient, Update command)
        {
            throw new Exception("I'm branch!");
        }
    }
}
