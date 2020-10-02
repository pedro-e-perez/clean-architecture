using Autofac;
using Autofac.Extensions.DependencyInjection;
using Library.Core.Entities;
using Library.Core.Services;
using Library.Core.SharedKernel;
using Library.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Library.Infrastructure
{
    public class ContainerSetup : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method. All of this starts
            // with the services.AddAutofac() that happens in Program and registers Autofac
            // as the service provider.
            var coreAssembly = Assembly.GetAssembly(typeof(BaseEntity));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(EfRepository));
            builder.RegisterAssemblyTypes(coreAssembly
                , infrastructureAssembly
                ).AsImplementedInterfaces();

        }
        public static IServiceProvider InitializeWeb(Assembly webAssembly, IServiceCollection services) =>
            new AutofacServiceProvider(BaseAutofacInitialization(setupAction =>
            {
                setupAction.Populate(services);
                setupAction.RegisterAssemblyTypes(webAssembly).AsImplementedInterfaces();
            }));

        public static IContainer BaseAutofacInitialization(Action<ContainerBuilder> setupAction = null)
        {
            var builder = new ContainerBuilder();

            var coreAssembly = Assembly.GetAssembly(typeof(BaseEntity));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(EfRepository));
            var repositoryLibros = Assembly.GetAssembly(typeof(RepositoryLibros));
            var libros = Assembly.GetAssembly(typeof(Libros));
            var serviceLibros = Assembly.GetAssembly(typeof(ServiceLibros));


            builder.RegisterAssemblyTypes(coreAssembly
                , infrastructureAssembly
                , repositoryLibros
                , libros
                , serviceLibros
                ).AsImplementedInterfaces();

            setupAction?.Invoke(builder);
            return builder.Build();
        }
    }
}
