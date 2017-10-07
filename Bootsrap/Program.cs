using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Bootstrap
{
    public class Program
    {
        // NB Create Readme.md

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
