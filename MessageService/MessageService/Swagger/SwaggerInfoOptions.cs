namespace MessageService.Swagger
{
    /// <summary>
    /// Сеттинги для формирования Swagger-документации.
    /// </summary>
    public class SwaggerInfoOptions
    {
        /// <summary>
        /// Префикс маршрута докуметации.
        /// </summary>
        public string RoutePrefix { get; set; }

        /// <summary>
        /// Название сервиса.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Описание сервиса.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Навание спецификации.
        /// </summary>
        public string DocumentTitle { get; set; }
    }
}