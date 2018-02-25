using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SEPTAInquirer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 5050, listenOptions =>
                    {
                        listenOptions.UseHttps("localhost.pfx", "881107");
                    });
                })
                .UseStartup<Startup>()
                .Build();
    }
}
