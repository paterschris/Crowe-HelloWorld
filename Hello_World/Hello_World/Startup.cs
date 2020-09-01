using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Hello_World.Models;
using Hello_World.Configuration;
using Microsoft.Extensions.Options;
using static Hello_World.Configuration.GlobalConfigurationSettings;

namespace Hello_World
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
            services.Configure<DatabaseConfig>(Configuration.GetSection("DatabaseConfig"));
            // Ability to change the location of the storage of the messages

            var databaseLocation = Configuration.GetValue<string>("DatabaseConfig:Location");
            var databaseName = Configuration.GetValue<string>("DatabaseConfig:Name");

            if (databaseLocation.Equals("Local"))
            {
                services.AddDbContext<MessageContext>(opt =>
                    opt.UseInMemoryDatabase(databaseName));
            }
            else
            {
                // This will never be run
                services.AddDbContext<MessageContext>(opt =>
                    opt.UseSqlite("NOT A REAL CONNECTION STRING"));
            }
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
