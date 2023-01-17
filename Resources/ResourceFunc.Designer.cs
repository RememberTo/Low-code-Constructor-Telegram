﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatbotConstructorTelegram.Resources {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ResourceFunc {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceFunc() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ChatbotConstructorTelegram.Resources.ResourceFunc", typeof(ResourceFunc).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на await bot.send_document(message.chat.id, open((&apos;PATH&apos;), &apos;rb&apos;), caption=&apos;name&apos;).
        /// </summary>
        internal static string BotSendDocument {
            get {
                return ResourceManager.GetString("BotSendDocument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на await bot.send_message(message.chat.id, &apos;name&apos;).
        /// </summary>
        internal static string BotSendMessage {
            get {
                return ResourceManager.GetString("BotSendMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на await bot.send_photo(message.chat.id, photo=open(&apos;PATH&apos;,&apos;rb&apos;), caption=&apos;name&apos;).
        /// </summary>
        internal static string BotSendPhoto {
            get {
                return ResourceManager.GetString("BotSendPhoto", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на await bot.send_message(call.message.chat.id, &apos;name&apos;).
        /// </summary>
        internal static string CallbackMessage {
            get {
                return ResourceManager.GetString("CallbackMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на @dp.callback_query_handler.
        /// </summary>
        internal static string CallbackQuery {
            get {
                return ResourceManager.GetString("CallbackQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на @dp.message_handler.
        /// </summary>
        internal static string Message {
            get {
                return ResourceManager.GetString("Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на commands=[&apos;name&apos;].
        /// </summary>
        internal static string ParamCommand {
            get {
                return ResourceManager.GetString("ParamCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на content_types=[&apos;name&apos;].
        /// </summary>
        internal static string ParamContentType {
            get {
                return ResourceManager.GetString("ParamContentType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на text=&apos;name&apos;.
        /// </summary>
        internal static string ParamForCallbackQuery {
            get {
                return ResourceManager.GetString("ParamForCallbackQuery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на lambda message: message.text == &apos;name&apos;.
        /// </summary>
        internal static string ParamLambdaMessage {
            get {
                return ResourceManager.GetString("ParamLambdaMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на await message.reply(&apos;name&apos;).
        /// </summary>
        internal static string ReplyMessage {
            get {
                return ResourceManager.GetString("ReplyMessage", resourceCulture);
            }
        }
    }
}