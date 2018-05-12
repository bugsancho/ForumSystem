namespace ForumSystem.Web
{
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;

    using ForumSystem.Core.Data;
    using ForumSystem.Identity.Managers;
    using ForumSystem.Infrastructure.Data;

    using Microsoft.AspNet.Identity.Owin;

    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterType<ApplicationUserManager>();
            //builder.RegisterType<ApplicationSignInManager>();
            builder.Register(context => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>());
            builder.Register(context => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
            builder.Register(context => HttpContext.Current.GetOwinContext().Authentication);

            builder.RegisterControllers(Assembly.GetCallingAssembly()).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IRepository<>).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(EfRepository<>).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<ForumSystemDbContext>().InstancePerLifetimeScope();

            IContainer container = builder.Build();
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}