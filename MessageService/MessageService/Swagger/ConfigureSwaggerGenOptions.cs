using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.Swagger
{
    /// <summary>
    /// Настройка параметров генерации Swagger.
    /// </summary>
    /// <remarks>Это позволяет версиям API определять документ Swagger для каждой версии API после.
    /// <see cref="IApiVersionDescriptionProvider"/> сервис был разрешен из сервисного контейнера.</remarks>
    public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IReadOnlyList<ApiVersionDescription> _apiVersionDescriptions;
        private readonly SwaggerInfoOptions _swaggerInfoOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerGenOptions"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"></see>.</param>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerGenOptions(IConfiguration configuration, IApiVersionDescriptionProvider provider)
        {
            _swaggerInfoOptions = configuration?.GetSection(nameof(SwaggerInfoOptions))?.Get<SwaggerInfoOptions>() ??
                throw new ArgumentNullException(nameof(configuration));
            _apiVersionDescriptions = provider?.ApiVersionDescriptions ??
                 throw new ArgumentNullException(nameof(provider));
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        /// <summary>
        /// Создание информации к версии спецификации.
        /// </summary>
        /// <param name="description"><see cref="ApiVersionDescription"/>.</param>
        /// <returns><see cref="Info"/>.</returns>
        private Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info
            {
                Title = _swaggerInfoOptions.ServiceName,
                Version = description.GroupName,
                Description = _swaggerInfoOptions.Description,
            };

            if (description.IsDeprecated)
            {
                info.Description += $" **Версия {description.GroupName} скоро не будет поддерживаться.**";
            }

            return info;
        }
    }
}