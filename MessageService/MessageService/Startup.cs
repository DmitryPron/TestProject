using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddOptions();

            services.AddHealthChecks();            


            services.AddControllers();
            services.RegisterSwagger();
            services.AddMvc(x => x.EnableEndpointRouting = false);
            services.AddDbContextPool<Context>(o => o.UseSqlServer("Persist Security Info=False;Integrated Security=true;Initial Catalog=Test_db; server=(local);"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.RegisterSwagger();            
            app.UseMvcWithDefaultRoute();
        }
    }
}
