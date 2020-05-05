using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace MessageService
{
    /// <summary>Расширение для swagger.</summary>
    public static class StartupSwagger
    {
        /// <summary>Зарегистрировать swagger.</summary>
        /// <param name="services">services.</param>
        /// <returns>Коллекция сервисов.</returns>
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
    {
        var serviceName = EnvironmentConfig.ServiceName;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(serviceName, new OpenApiInfo { Title = serviceName, Version = "v1", Description = "Описание сервиса" });
                c.DescribeAllEnumsAsStrings();
                c.DescribeAllParametersInCamelCase();
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Documentation.xml"));
            });

            return services;
    }

    /// <summary>Зарегистрировать swagger.</summary>
    /// <param name="app">app.</param>
    /// <returns>builder.</returns>
    public static IApplicationBuilder RegisterSwagger(this IApplicationBuilder app)
    {
        var serviceName = EnvironmentConfig.ServiceName;
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint(
                EnvironmentConfig.IsLocal
                    ? $"/swagger/{serviceName}/swagger.json"
                    : $"/{serviceName}/swagger/{serviceName}/swagger.json", serviceName);
            c.DisplayRequestDuration();
            c.DisplayOperationId();
        });
        return app;
    }
}
}