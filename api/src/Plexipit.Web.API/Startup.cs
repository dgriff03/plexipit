using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plexipit.Data.Dapper.Modules;
using Plexipit.Web.API.Managers;

namespace Plexipit.Web.API
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
            // Add framework services
            services.AddMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connString = Configuration.GetConnectionString("DefaultConnection");

            builder.RegisterType<PodcastModule>()
                   .WithParameter("connectionString", connString)
                   .AsSelf();

            builder.RegisterType<EpisodeModule>()
                   .WithParameter("connectionString", connString)
                   .AsSelf();

            builder.RegisterType<PodcastsManager>().AsSelf();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "Podcasts",
                    template: "{controller=Podcasts}/{action=Index}/{id?}");
            });
        }
    }
}
