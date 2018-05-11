namespace ForumSystem.Web.App_Start
{
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;

    using ForumSystem.Identity.Managers;

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
            builder.RegisterControllers(Assembly.GetCallingAssembly());

            IContainer container = builder.Build();
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
        }
    }
}