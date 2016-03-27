using Chatterbox.Model.Repositories;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Chatterbox.Web
{
    public class Startup
    {
        private const string ConnectionStringConfigKey = "Data:DefaultConnection:ConnectionString";

        private readonly IConfigurationRoot _configuration;

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("project.json")
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGarmentRepository, GarmentRepository>((s) =>
            {
                return new GarmentRepository(_configuration[ConnectionStringConfigKey]);
            });

            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Wardrobe", action = "Garments" });
            });
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
