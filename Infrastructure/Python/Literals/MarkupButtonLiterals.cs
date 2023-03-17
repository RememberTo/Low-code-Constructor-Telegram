using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotConstructorTelegram.Infrastructure.Python.Literals
{
    internal class MarkupButtonLiterals
    {
        public static string InizializeOpen = "markup_reply = types.ReplyKeyboardMarkup(resize_keyboard=True, keyboard=[";
        public static string InizializeClose = "])";
        public static string LinebuttonsOpen = "[";
        public static string LinebuttonsClose = "],";

        public static string ViewButton = "types.KeyboardButton(text='TEXT'),";
        public static string SendText = "await bot.send_message(message.chat.id, text='name', reply_markup=markup_reply)";
        public static string SendPhoto = "await bot.send_photo(message.chat.id, open(('PATH'), 'rb'), caption='name', reply_markup = markup_reply)";
        public static string SendDocument = "await bot.send_document(message.chat.id, open(('PATH'), 'rb'), caption='name', reply_markup = markup_reply)";
        public static string DecoratorFunction = "@dp.message_handler(lambda message: message.text == 'NAME')";
        public static string HeadFunction = "async def FUNC(message: types.Message):";
    }
}
