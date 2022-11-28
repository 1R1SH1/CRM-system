using Bot.Core;
using Bot.Models;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot
{
    public static class Program
    {
        private static TelegramBotClient _bot;
        public static async Task Main(string[] args)
        {
            string token = System.IO.File.ReadAllText(@"C:\Users\Rishat\Desktop\Token\Token.txt");

            _bot = new TelegramBotClient(token);

            var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            _bot.StartReceiving(
                UpdateHandler,
                ErrorHandler,
                receiverOptions,
                cancellationToken: cts.Token);

            var me = await _bot.GetMeAsync();

            Console.ReadLine();

            cts.Cancel();

            Console.ReadLine();
        }

        static Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Ошибка телеграм АПИ:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandleMessage(bot, update, cancellationToken);
                return;
            }
        }

        static string name;
        static string surName;
        static string eMail;
        static string information;

        static async Task HandleMessage(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            // Тут бот получает сообщения от пользователя
            // Дальше код отвечает за команду старт, которую можно добавить через botfather
            // Если все хорошо при запуске program.cs в консоль выведется, что бот запущен
            // а при отправке команды бот напишет Привет
            if (update.Message.Text == "Старт")
            {
                switch (update.Message.Text)
                {
                    case "Старт":
                        ReplyKeyboardMarkup keyboard1 = new(new[]
                {
                            new KeyboardButton[] { "Оставить заявку"},
                            new KeyboardButton[] { "Проекты" },
                            new KeyboardButton[] { "Услуги" },
                            new KeyboardButton[] { "Блог" },
                            new KeyboardButton[] { "Контакты" }
                        })
                        {
                            ResizeKeyboard = true
                        };
                        await bot.SendTextMessageAsync(update.Message.Chat.Id, $"Добрый день, мы рады, что вы с нами!" + "\n" +
                                                                               $"Выберите кнопки:", replyMarkup: keyboard1, cancellationToken: cancellationToken);
                        break;
                }
            }

            if (update.Message.ReplyToMessage != null && update.Message.ReplyToMessage.Text.Contains("Введите ваше Имя:"))
            {
                if (update.Type == UpdateType.Message)
                {
                    if (update.Message.Type == MessageType.Text)
                    {
                        //write an update
                        name = update.Message.Text;
                    }
                }

                await bot.SendTextMessageAsync(update.Message.Chat.Id, "Введите вашу Фамилию:", replyMarkup: new ForceReplyMarkup { Selective = true });

            }

            if (update.Message.ReplyToMessage != null && update.Message.ReplyToMessage.Text.Contains("Введите вашу Фамилию:"))
            {

                if (update.Type == UpdateType.Message)
                {
                    if (update.Message.Type == MessageType.Text)
                    {
                        //write an update
                        surName = update.Message.Text;
                    }
                }

                await bot.SendTextMessageAsync(update.Message.Chat.Id, "Введите вашу Электронную почту:", replyMarkup: new ForceReplyMarkup { Selective = true });

            }

            if (update.Message.ReplyToMessage != null && update.Message.ReplyToMessage.Text.Contains("Введите вашу Электронную почту:"))
            {

                if (update.Type == UpdateType.Message)
                {
                    if (update.Message.Type == MessageType.Text)
                    {
                        //write an update
                        eMail = update.Message.Text;
                    }
                }

                await bot.SendTextMessageAsync(update.Message.Chat.Id, "Задайте вопрос:", replyMarkup: new ForceReplyMarkup { Selective = true });

            }

            if (update.Message.ReplyToMessage != null && update.Message.ReplyToMessage.Text.Contains("Задайте вопрос:"))
            {

                if (update.Type == UpdateType.Message)
                {
                    if (update.Message.Type == MessageType.Text)
                    {
                        //write an update                        
                        information = update.Message.Text;
                    }
                }
                CRUD.Create("Request", JsonConvert.SerializeObject(new Requests(0, name, surName, eMail, information, DateTime.Now)));

                await bot.SendTextMessageAsync(update.Message.Chat.Id, "Регистрация окончена", replyMarkup: new ForceReplyMarkup { Selective = true });

            }

            if (update.Message.Type == MessageType.Text)
            {
                switch (update.Message.Text)
                {
                    case "Оставить заявку":
                        await bot.SendTextMessageAsync(
                              chatId: update.Message.Chat.Id,
                              text: "Введите ваше Имя:",
                              replyMarkup: new ForceReplyMarkup { Selective = true },
                              cancellationToken: cancellationToken);

                        break;

                    case "Проекты":
                        var projects = JsonConvert.DeserializeObject<List<Projects>>(CRUD.Read("Project"));
                        foreach (var project in projects)
                        {
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, project.Image, cancellationToken: cancellationToken);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, project.Header, cancellationToken: cancellationToken);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, project.ProjectInformation, cancellationToken: cancellationToken);
                        }
                        break;
                    case "Услуги":
                        var services = JsonConvert.DeserializeObject<List<Services>>(CRUD.Read("Services"));
                        foreach (var service in services)
                        {
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, service.Header, cancellationToken: cancellationToken);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, service.ServicesInformation, cancellationToken: cancellationToken);
                        }
                        break;
                    case "Блог":
                        var blogs = JsonConvert.DeserializeObject<List<Blogs>>(CRUD.Read("Blog"));
                        foreach (var blog in blogs)
                        {
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, blog.Image, cancellationToken: cancellationToken);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, blog.Header, cancellationToken: cancellationToken);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, blog.BlogInformation, cancellationToken: cancellationToken);
                        }
                        break;
                    case "Контакты":
                        var contacts = JsonConvert.DeserializeObject<List<Contacts>>(CRUD.Read("Contacts"));
                        foreach (var contact in contacts)
                        {
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, contact.Image, cancellationToken: cancellationToken);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, contact.Address, cancellationToken: cancellationToken);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, contact.ContactsInformation, cancellationToken: cancellationToken);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, contact.EMail, cancellationToken: cancellationToken);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, contact.Phone, cancellationToken: cancellationToken);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, contact.Name, cancellationToken: cancellationToken);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, contact.SurName, cancellationToken: cancellationToken);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, contact.LastName, cancellationToken: cancellationToken);
                        }
                        break;
                }
            }
        }
    }
}


