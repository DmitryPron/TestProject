using MessageService.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace MessageService
{
    public class Startup
    {
        /// <summary>Initializes a new instance of the <see cref="Startup"/> class.</summary>
        /// <param name="configuration">Конфигурация <see cref="IConfiguration"/>.</param>
        /// <param name="hostingEnvironment">Среда <see cref="IConfiguration"/>.</param>
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        /// <summary>Среда <see cref="IHostingEnvironment"/>.</summary>
        private IHostingEnvironment HostingEnvironment { get; }

        /// <summary>Конфигурация <see cref="IConfiguration"/>.</summary>
        private IConfiguration Configuration { get; }

        /// <summary>This method gets called by the runtime. Use this method to add services to the container.</summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

            if (!HostingEnvironment.IsProduction())
            {
                services.AddOwnSwagger();
            }
        }

        /// <summary>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        /// <param name="provider"><see cref="IApiVersionDescriptionProvider"/></param>
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseHealthChecks("/health");
            app.UseAuthentication();

            if (!HostingEnvironment.IsProduction())
            {
                app.UseOwnSwagger(Configuration, provider);
            }

            app.UseMvc();
        }
    }
}
