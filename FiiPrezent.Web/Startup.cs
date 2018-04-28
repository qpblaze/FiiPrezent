using System.Globalization;
using AutoMapper;
using FiiPrezent.Core.Entities;
using FiiPrezent.Core.Interfaces;
using FiiPrezent.Infrastructure;
using FiiPrezent.Infrastructure.Data;
using FiiPrezent.Infrastructure.Hubs;
using FiiPrezent.Infrastructure.Services;
using FiiPrezent.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FiiPrezent.Web
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

            services.AddAutoMapper();

            var cultureInfo = new CultureInfo("ro-RO");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

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
                 });

            services.AddScoped<IParticipantsUpdated, ParticipantsUpdated>();

            services.AddTransient<IRepository<Participant>, EfRepository<Participant>>();
            services.AddTransient<IRepository<Event>, EfRepository<Event>>();
            services.AddTransient<IRepository<Account>, EfRepository<Account>>();

            services.AddTransient<IParticipantsService, ParticipantsService>();
            services.AddTransient<IEventsService, EventsService>();
            services.AddTransient<IAccountsService, AccountsService>();

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