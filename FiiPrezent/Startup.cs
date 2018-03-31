using FiiPrezent.Hubs;
using FiiPrezent.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FiiPrezent
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddMvc();

            services.AddScoped<EventsService>();
            services.AddScoped<IParticipantsUpdated, ParticipantsUpdated>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(routes =>
            {
                routes.MapHub<UpdateParticipants>("/participants");
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}