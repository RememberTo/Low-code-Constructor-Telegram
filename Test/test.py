from aiogram import Bot, Dispatcher, executor, types
bot = Bot(token='5154449316:AAEE_JeL9Aha81J-jn4anTOkziuCgdM3Q5w')
dp = Dispatcher(bot=bot)

print('Bot start')


@dp.message_handler(commands=['start'])
async def start_handler(message):
	await bot.send_message(message.chat.id, 'Привет')

@dp.message_handler(commands=['img'])
async def img_handler(message):
	await bot.send_photo(message.chat.id, photo=open('D:\\\\Images\\\\5-2.jpg','rb'), caption='Это твоя пикча')

@dp.message_handler(commands=['ph'])
async def ph_handler(message):
	await bot.send_message(message.chat.id, 'Пошел от сюда')

if __name__ == '__main__':
    executor.start_polling(dp)
