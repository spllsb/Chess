using System.Reflection;
using Autofac;
using Chess.Infrastructure.EF;

namespace Chess.Infrastructure.IoC.Modules
{
    public class DatabaseModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(DatabaseModule)
                .GetTypeInfo()
                .Assembly;
            
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IDatabaseRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}