using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MessageService
{
    public class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="args">arguments of string[].</param>
        public static void Main(string[] args)
            => CreateWebHostBuilder(args).Build().Run();

        /// <summary>
        /// <see cref="WebHost"/> Билдер <see cref="IWebHostBuilder"/>.
        /// </summary>
        /// <param name="args">arguments of string[].</param>
        /// <returns>static <see cref="IWebHostBuilder"/>.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)

                // TODO: Модуль сервера конфигурации Spring Cloud Config Использует статичное файловое хранилище (зашитое в JAR'нике),
                // для хранения конфигов (вместо использования репозитория GIT).

                // .UseSpringCloudConfig()
                .UseStartup<Startup>();
    }
}
