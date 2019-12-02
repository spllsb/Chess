using System.Reflection;
using Autofac;
using Chess.Infrastructure.Commands;

namespace Chess.Infrastructure.IoC.Modules
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>() 
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();

            //Dzięki użyciu autofac oraz reflekscji
            //nie musimy defitniować wszyskich zależności ręcznie:
            
            // -------------------------------------
            // ręczna definicja przykładowa
            // builder.RegisterType<CreateUserHandler>()
            //     .As<ICommandHandler<CreateUser>>()
            //     .InstancePerLifetimeScope();
            //--------------------------------------
        }
    }
}