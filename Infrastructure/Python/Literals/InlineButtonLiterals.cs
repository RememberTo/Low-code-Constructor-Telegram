using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotConstructorTelegram.Infrastructure.Python.Literals
{
    internal class InlineButtonLiterals
    {
        public static string InizializeOpen = "markup_inline = types.InlineKeyboardMarkup(row_width = 2, inline_keyboard =[";
        public static string InizializeClose = "])";
        public static string LinebuttonsOpen = "[";
        public static string LinebuttonsClose = "],";

        public static string ViewButton = "types.InlineKeyboardButton(text='TEXT', url='NONE', callback_data='UNIQUE_ID'),";
        public static string SendText = "await bot.send_message(call.message.chat.id, text='name', reply_markup=markup_inline)";
        public static string SendPhoto = "await bot.send_photo(call.message.chat.id, open(('PATH'), 'rb'), caption='name', reply_markup = markup_inline)";
        public static string SendDocument = "await bot.send_document(call.message.chat.id, open(('PATH'), 'rb'), caption='name', reply_markup = markup_inline)";
        public static string DecoratorFunction = "@dp.callback_query_handler(text='UNIQUEID')";
        public static string HeadFunction = "async def answer(call: types.CallbackQuery):";


    }
}
