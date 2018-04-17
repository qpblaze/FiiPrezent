using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FiiPrezent.Web
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
            
            host.Run();
        }
    }
 }
