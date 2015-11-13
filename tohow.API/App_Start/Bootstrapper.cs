using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using tohow.Data.DbContext;
using tohow.Data.Repository;
using tohow.Interface.DbContext;
using tohow.Interface.Repository;
using tohow.Service;

namespace tohow.API.App_Start
{
    public static class Bootstrapper
    {
        private static IContainer _container;

        public static void Run() { SetAutoFacContainer(); }

        private static void SetAutoFacContainer() {
            var container = GetContainer();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public static IContainer GetContainer() {

            if (_container != null) return _container;

            var containerBuilder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            // Autofac
            containerBuilder.RegisterModule<AutofacWebTypesModule>();
            containerBuilder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();
            containerBuilder.RegisterWebApiFilterProvider(config);

            // register db context
            containerBuilder.RegisterType<TohowDevDbContext>().As<ITohowDevDbContext>().InstancePerLifetimeScope();
            // register repository
            containerBuilder.RegisterType<TohowDevRepository>().AsImplementedInterfaces();
            // register services
            containerBuilder.RegisterType<TohowService>().AsImplementedInterfaces().InstancePerDependency();

            _container = containerBuilder.Build();
            return _container;
        }
    }
}