using System.Text;
using Autofac;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            // Handle auth
            //services.AddAuthentication()
                //.AddFacebook(options =>
                //{
                //    options.AppId = Configuration["auth:facebook:appid"];
                //    options.AppSecret = Configuration["auth:facebook:appsecret"];
                //})
                //.AddGoogle(options =>
                //{
                //    options.ClientId = Configuration["auth:google:clientid"];
                //    options.ClientSecret = Configuration["auth:google:clientsecret"];
                //})
                //.AddTwitter(options =>
                //{
                //    options.ConsumerKey = Configuration["auth:twitter:consumerkey"];
                //    options.ConsumerSecret = Configuration["auth:twitter:consumersecret"];
                //});

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

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
