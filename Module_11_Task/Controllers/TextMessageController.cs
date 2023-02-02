using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace Module_11_Task.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            if (message.Text == "/start")
            {
                Console.WriteLine("Отправлено меню выбора");

                // Объект, представляющий кноки
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]
                {
                        InlineKeyboardButton.WithCallbackData($" Длинна сообщения" , $"LM"),
                        InlineKeyboardButton.WithCallbackData($" Сумма чисел" , $"Sum")
                    });

                // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"  Мой бот умеет считать длинну сообщения или сумму цифр. {Environment.NewLine}" +
                    $"{Environment.NewLine}Выбери действие.{Environment.NewLine}", cancellationToken: ct, replyMarkup: new InlineKeyboardMarkup(buttons));
            }
        }
    }
}