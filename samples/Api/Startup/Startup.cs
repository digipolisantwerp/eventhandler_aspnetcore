using System.IO;
using Api.Shared.Extensions;
using Api.Shared.Options;
using Digipolis.Eventhandler.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.Startup
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            ApplicationBasePath = env.ContentRootPath;
            ConfigPath = Path.Combine(ApplicationBasePath, "_config");

            var builder = new ConfigurationBuilder()
                .SetBasePath(ConfigPath)
                .AddJsonFile("logging.json")
                .AddJsonFile("app.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; private set; }

        public string ApplicationBasePath { get; private set; }

        public string ConfigPath { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Check out ExampleController to find out how these configs are injected into other classes
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //TODO: Enable lines in comment below once Toolbox.WebApi is upgraded to .Net Core 1.0.0
            services.AddMvc();//.AddActionOverloading().AddVersioning();

            services.AddEventHandler(opt => opt.FileName = ConfigPath + @"\eventhandlerconfig.json");

            services.AddBusinessServices();
            services.AddAutoMapper();
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSeriLog(Configuration.GetSection("SeriLog"));
            loggerFactory.AddConsole(Configuration.GetSection("ConsoleLogging"));
            loggerFactory.AddDebug(LogLevel.Debug);


            // CORS
            app.UseCors((policy) =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
                policy.AllowCredentials();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
