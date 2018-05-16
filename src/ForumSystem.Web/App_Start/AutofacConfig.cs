namespace ForumSystem.Web
{
    using System;
    using System.Configuration;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;

    using ForumSystem.Core.Analytics;
    using ForumSystem.Core.Data;
    using ForumSystem.Core.Users;
    using ForumSystem.Identity.Managers;
    using ForumSystem.Identity.Providers;
    using ForumSystem.Infrastructure.Cache;
    using ForumSystem.Infrastructure.Data;

    using Microsoft.AspNet.Identity.Owin;

    using WebGrease;

    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.Register(context => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>());
            builder.Register(context => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
            builder.Register(context => HttpContext.Current.GetOwinContext().Authentication);

            builder.RegisterApiControllers(Assembly.GetCallingAssembly()).InstancePerLifetimeScope();
            builder.RegisterControllers(Assembly.GetCallingAssembly()).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IRepository<>).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(EfRepository<>).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<ForumSystemDbContext>().WithParameter(new TypedParameter(typeof(string), "ForumSystemDbConnection")).InstancePerLifetimeScope();

            builder.RegisterType<PermissionsService>().As<IPermissionsService>();
            // For the cache to be useful, we need to have a single instance, shared accross requests
            builder.RegisterType<InMemoryCacheManager>().As<ICacheManager>().SingleInstance();

            builder.RegisterType<FileThreadStatisticsRepository>().As<IThreadStatisticsRepository>().WithParameter(
                (param, context) => param.Name == "filePath",
                (info, context) =>
                    {
                        string settingKey = "ThreadStatisticsDataFileName";
                        string statisticsRelativePath = ConfigurationManager.AppSettings[settingKey];
                        if (string.IsNullOrWhiteSpace(statisticsRelativePath))
                        {
                            throw new ArgumentException($"Please provide a value for the '{settingKey}' AppSetting");
                        }

                        return HttpContext.Current.Server.MapPath(statisticsRelativePath);
                    });


            IContainer container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}