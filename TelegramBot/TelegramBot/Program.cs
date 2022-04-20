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

        private static CommandHandler handler;

        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);

            var end = new UnknowenCommandHandler();

            var handler1 = new ApiCommandHandler("Общее время работы с разбивкой по ионам", end);
            var handler2 = new ApiCommandHandler("Время начала работ по договору", handler1);

            var handler3 = new GetButtonCommandHandler("Информация по договору", ConstNameButton.names["Информация по договору"], handler2);


            handler = new GetButtonCommandHandler("кнопки", ConstNameButton.names.Keys, handler3);
          

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
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update)); //данные о консоли(удоли)

            var message = update.Message;
            await handler.HandleCommand(botClient, update);

            //if (ConstNameButton.names.ContainsKey(message.Text))
            //{
            //    await botClient.SendTextMessageAsync(message.Chat.Id, "Команда выполнена", replyMarkup: GetButtons(ConstNameButton.names[message.Text]));
            //}
            //else
            //{
            //    await botClient.SendTextMessageAsync(message.Chat.Id, message.Text, replyMarkup: GetButtons(ConstNameButton.names.Keys));
            //}
        }

        private static IReplyMarkup GetButtons(IEnumerable<string> names)
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

        public Task HandleCommand(ITelegramBotClient botClient, Update command)
        {
            if (CanHandle(command))
            {
                return OnHnadle(botClient, command);
            }
            else
            {
                if (next == null) 
                {
                    throw new Exception("Unknown command");
                }
                return next.HandleCommand(botClient, command);
            }
        }

        protected abstract bool CanHandle(Update command);
        protected abstract Task OnHnadle(ITelegramBotClient botClient, Update command);
    }

    class UnknowenCommandHandler : CommandHandler
    {
        protected override bool CanHandle(Update command)
        {
            return true;
        }

        protected override Task OnHnadle(ITelegramBotClient botClient, Update command)
        {
            var chatID = command.Message.Chat.Id;
            var messageText = "Не известна команда " + command.Message.Text;

            return botClient.SendTextMessageAsync(chatID, messageText, replyMarkup: GetButtons(ConstNameButton.names.Keys));
        }

        private static IReplyMarkup GetButtons(IEnumerable<string> names)
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

    class ApiCommandHandler : CommandHandler
    {
        private string command;
        public ApiCommandHandler(string command, CommandHandler next) : base(next)
        {
            this.command = command;
        }

        protected override bool CanHandle(Update command)
        {
            return this.command == command.Message.Text;
        }

        protected override Task OnHnadle(ITelegramBotClient botClient, Update command)
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

        protected override bool CanHandle(Update command)
        {
            return this.command == command.Message.Text;
        }

        protected override Task OnHnadle(ITelegramBotClient botClient, Update command)
        {
            var chatID = command.Message.Chat.Id;
            var messageText = "выполнено";

            return botClient.SendTextMessageAsync(chatID, messageText, replyMarkup: GetButtons(result));
        }

        private static IReplyMarkup GetButtons(IEnumerable<string> names)
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

}
