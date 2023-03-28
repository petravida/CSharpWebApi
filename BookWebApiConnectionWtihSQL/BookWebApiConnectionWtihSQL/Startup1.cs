using Autofac;
using Autofac.Integration.WebApi;
using BookConnecion.Repository;
using BookConnection.Repository.common;
using BookConnection.Service;
using BookConnection.Service.common;
using BookWebApiConnectionWtihSQL.Controllers;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using System;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(BookWebApiConnectionWtihSQL.Startup1))]

namespace BookWebApiConnectionWtihSQL
{
    public class Startup1
    {
        public static void Configuration()
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<BookService>().As<IBookService>();
            builder.RegisterType<BookRepository>().As<IBookRepository>();
            builder.RegisterType<BookController>();

            IContainer container = builder.Build();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
