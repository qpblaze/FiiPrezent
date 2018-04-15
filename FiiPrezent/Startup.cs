using FiiPrezent.Data;
using FiiPrezent.Entities;
using FiiPrezent.Hubs;
using FiiPrezent.Interfaces;
using FiiPrezent.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FiiPrezent
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("secrets.json", true, true)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddDbContext<ApplicationDbContext>(optiions =>
            {
                optiions.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(o => o.LoginPath = new PathString("/login"))
                 .AddFacebook(o =>
                 {
                     o.AppId = Configuration["Authentication:Facebook:AppId"];
                     o.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                     o.Scope.Add("public_profile");
                     o.Fields.Add("picture");
                 });

            services.AddScoped<IParticipantsUpdated, ParticipantsUpdated>();

            services.AddTransient<IRepository<Event>, Repository<Event>>();
            services.AddTransient<IRepository<Participant>, Repository<Participant>>();
            services.AddTransient<IParticipantsService, ParticipantsService>();
            services.AddTransient<IEventsService, EventsService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<ParticipantsHub>("/participants");
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}