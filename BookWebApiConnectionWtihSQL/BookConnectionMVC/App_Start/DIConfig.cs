using Autofac;
using Autofac.Integration.Mvc;
using BookConnecion.Repository;
using BookConnection.Repository.common;
using BookConnection.Service;
using BookConnection.Service.common;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BookConnectionMVC.App_Start
{
    public class DIConfig
    {
        public static void Configuration()
        {

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<BookService>().As<IBookService>();
            builder.RegisterType<EFBookRepository>().As<IBookRepository>();
            builder.RegisterType<BookContext>().AsSelf().InstancePerLifetimeScope();


            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}