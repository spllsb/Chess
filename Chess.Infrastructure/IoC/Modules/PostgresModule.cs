using System.Reflection;
using Autofac;
using Chess.Infrastructure.EF;

namespace Chess.Infrastructure.IoC.Modules
{
    public class PostgresModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(PostgresModule)
                .GetTypeInfo()
                .Assembly;
            
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IPostgresRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}