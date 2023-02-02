using Module_11_Task.Services;
using System.Threading.Tasks;
using System.Threading;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Module_11_Task.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            
            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).ActionCode = callbackQuery.Data;
            // Генерим информационное сообщение
            string ActionText = callbackQuery.Data 
                switch
            {
                "LM" => " Длинна сообщения",
                "Sum" => " Сумма чисел",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"Выбрано - {ActionText}.{Environment.NewLine}" +
                $"{Environment.NewLine}Можно изменить в главном меню." , cancellationToken: ct);
            
            
        }
    }
}