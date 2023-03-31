using Autofac.Integration.WebApi;
using Autofac;
using BookConnecion.Repository;
using BookConnection.Repository.common;
using BookConnection.Service.common;
using BookConnection.Service;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace BookWebApiConnectionWtihSQL.App_Start
{
    public class DIConfig
    {
        public static void Configuration()
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<BookService>().As<IBookService>();
            builder.RegisterType<EFBookRepository>().As<IBookRepository>();
            builder.RegisterType<BookContext>().AsSelf().InstancePerLifetimeScope();


            IContainer container = builder.Build();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}