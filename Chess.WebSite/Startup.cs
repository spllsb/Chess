using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Chess.Core.Domain;
using Chess.Infrastructure.EF;
using Chess.Infrastructure.Hubs;
using Chess.Infrastructure.IoC;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Chess.WebSite.Framework;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chess.WebSite
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
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSignalR();
            //Settings
            var identitySettingsSection = Configuration.GetSection("ChessGameSettings");
            services.Configure<ChessGameSettings>(identitySettingsSection);
            identitySettingsSection = Configuration.GetSection("DrillSettings");
            services.Configure<DrillSettings>(identitySettingsSection);
            identitySettingsSection = Configuration.GetSection("EmailSettings");
            services.Configure<EmailSettings>(identitySettingsSection);

            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<MyDbContext>(options => options
                    .UseNpgsql(Configuration.GetValue<string>("Database:ConnectionString"))
                    .UseSnakeCaseNamingConvention());
            // services.AddOptions();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<MyDbContext>();
            // services.AddIdentityCore<ApplicationUser>()
            //         .AddDefaultTokenProviders()
            //         .AddEntityFrameworkStores<MyDbContext>();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");

            services.AddHangfire(config => 
                config.UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());
            services.AddHangfireServer();
            services.AddSingleton<TestService>();
            services.AddSingleton<ChessMatchHub>();


            services.AddTransient<IEmailSender, MessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                            IWebHostEnvironment env,
                            IBackgroundJobClient backgroundJobClient,
                            IRecurringJobManager recurringJobManager,
                            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
             
            app.UseMyExceptionHandler();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard();
            backgroundJobClient.Enqueue(() => Console.WriteLine("Hello Hangfire!!"));
            recurringJobManager.AddOrUpdate(
                "CreateScheduleChessgame",
                () => serviceProvider.GetService<ChessMatchHub>().Test(),
                "* * * * *"
            );
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=Home}/{action=Index}/"
                );
                
                endpoints.MapHub<ChessMatchHub>("/chessMatchHub");
                endpoints.MapRazorPages();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterModule(new ContainerModule(Configuration));
        }
    }
}
