using System;
using System.Diagnostics.CodeAnalysis;

namespace ChatbotConstructorTelegram.Model.Bot
{
    internal static class DataProject
    {
        public static string? Name { get; set; }
        public static string? Description { get; set; }
        public static string? Token { get; set; }
        public static string? PathDirectory { get; set; }
        public static string? Path { get; set; }
        public static bool IsReadyAiogram { get; set; }
        public static bool IsReadyPython { get; set; }
    }
}
