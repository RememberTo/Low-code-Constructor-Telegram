from aiogram import Bot, Dispatcher, executor, types
bot = Bot(token='5154449316:AAEE_JeL9Aha81J-jn4anTOkziuCgdM3Q5w')
dp = Dispatcher(bot=bot)

print('Bot start')


@dp.message_handler(commands=['start'])
async def start_handler(message):
    await 
if __name__ == '__main__':
    executor.start_polling(dp)
