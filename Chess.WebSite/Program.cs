using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


//https://www.youtube.com/watch?v=uKS9Z2lAgT4&list=PL0kdOcU3HXGL5gvSHJX8sbCqbv3CaoYUb&index=8
//https://github.com/bilalshahzad139/asp.net-core3.0-web-api-learn-in-urdu/blob/master/CoreApiTesting/ApiConsumerTest/Views/Shared/_Layout.cshtml
namespace Chess.WebSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
