using System;

namespace MessageService
{
    /// <summary>Конфигурация среды.</summary>
    public class EnvironmentConfig
    {

        /// <summary>Получить код проекта.</summary>
        /// <returns>Возвращает строку.</returns>
        public static string ProjectKey => Environment.GetEnvironmentVariable("PROJECT_KEY")
            ?? throw new ApplicationException("Environment Variable PROJECT_KEY is null");

        /// <summary>Получить имя микросервиса.</summary>
        /// <returns>Возвращает строку.</returns>
        public static string ServiceName => Environment.GetEnvironmentVariable("SERVICE_NAME")
            ?? throw new ApplicationException("Environment Variable SERVICE_NAME is null");

        /// <summary>Получить имя переменной среды.</summary>
        /// <returns>Возвращает строку.</returns>
        public static string EnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        /// <summary>Получить проверку на запуск в локальной среде.</summary>
        /// <returns>Возвращает true, если запуск произошел в локальной среде.</returns>
        public static bool IsLocal => Convert.ToBoolean(Environment.GetEnvironmentVariable("LOCAL"));
    }
}
