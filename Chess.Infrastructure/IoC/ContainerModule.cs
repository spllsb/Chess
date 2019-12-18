using Autofac;
using Chess.Infrastructure.IoC.Modules;
using Chess.Infrastructure.Mappers;
using Microsoft.Extensions.Configuration;

namespace Chess.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize())
                .SingleInstance();
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<RepositoryModule>();
            // builder.RegisterModule<DatabaseModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterInstance(new SettingsModule(_configuration));


        }
    }
}