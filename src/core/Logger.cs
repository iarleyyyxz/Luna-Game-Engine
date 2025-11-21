using SDL2;

namespace Luna.Core
{
    public enum LogLevel
    {   INFO, WARN, ERROR, DEBUG }

    public struct LogMessage
    {
        public string Text;
        public SDL.SDL_Color Color;

        public LogMessage(string text, SDL.SDL_Color color)
        {
            Text = text;
            Color = color;
        }
    }

    public static class Logger
    {
        public enum LogLevel { Info, Warning, Error }

        private static readonly List<LogMessage> _messages = new List<LogMessage>();

        public static IReadOnlyList<LogMessage> Messages => _messages;

        private static SDL.SDL_Color GetColor(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Warning:
                    return new SDL.SDL_Color { r = 255, g = 255, b = 0 };   // Yellow
                case LogLevel.Error:
                    return new SDL.SDL_Color { r = 255, g = 80, b = 80 };  // Red
                default:
                    return new SDL.SDL_Color { r = 255, g = 255, b = 255 }; // White
            }
        }

        public static void Log(string message, LogLevel level = LogLevel.Info)
        {
            string formatted = $"[{level}] {message}";
            Console.WriteLine(formatted);

            SDL.SDL_Color color = GetColor(level);
            _messages.Add(new LogMessage(formatted, color));

            if (_messages.Count > 2000)
                _messages.RemoveAt(0);
        }
    }
}