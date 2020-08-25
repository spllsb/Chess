using Autofac;
using Chess.Infrastructure.EF;
using Chess.Infrastructure.Extensions;
using Chess.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Chess.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>())
                .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<ChessGameSettings>())
                .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<DatabaseSettings>())
                .SingleInstance();
        }
    }
}