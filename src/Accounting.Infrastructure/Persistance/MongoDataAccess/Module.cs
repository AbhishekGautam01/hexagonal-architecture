using Accounting.Infrastructure.Exceptions;
using Autofac;

namespace Accounting.Infrastructure.Persistance.MongoDataAccess
{
    public class Module: Autofac.Module
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>()
                .As<Context>()
                .WithParameter("connectionString", ConnectionString)
                .WithParameter("databseName", DatabaseName)
                .SingleInstance();

            // Register all Types in MongoDataAccess
            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
                .Where(type => type.Namespace.Contains("MongoDataAccess"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }



    }
}
