using MessageService.Swagger.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;

namespace MessageService.Swagger
{
    /// <summary>
    /// Расширения Swagger.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Добавить собственный Swagger.
        /// </summary>
        /// <param name="services">Коллеция услуг.</param>
        public static void AddOwnSwagger(this IServiceCollection services)
        {
            services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition(nameof(AuthenticationSchemes.Basic), new BasicAuthScheme());
                c.OperationFilter<OperationApiBasicSecurityFilter>();
                c.OperationFilter<OperationApiVersionFilter>();

                c.DescribeAllEnumsAsStrings();
                c.EnableAnnotations();
            });
        }

        /// <summary>
        /// Использовать собственный Swagger.
        /// </summary>
        /// <param name="app">Провайдер конфигурации приложения.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <param name="provider"><see cref="IApiVersionDescriptionProvider"/>.</param>
        public static void UseOwnSwagger(this IApplicationBuilder app, IConfiguration configuration, IApiVersionDescriptionProvider provider)
        {
            var swaggerInfoOptions = configuration.GetSection(nameof(SwaggerInfoOptions)).Get<SwaggerInfoOptions>();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = $"{swaggerInfoOptions.RoutePrefix}/{{documentName}}/swagger.json";
            });
        }
    }
}