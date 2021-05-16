using System;
using System.Collections.Generic;
using System.Text;
using Accounting.Infrastructure.Exceptions;
using Autofac;
namespace Accounting.Infrastructure.Persistance.InMemoryDataAccess
{
    public class Module: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>()
                .As<Context>()
                .SingleInstance();

            //
            // Register all Types in InMemoryDataAccess namespace
            //
            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
                    .Where(type => type.Namespace.Contains("InMemoryDataAccess"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
        }
    }
}
