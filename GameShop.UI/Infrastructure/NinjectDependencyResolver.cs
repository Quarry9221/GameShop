using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameShop.Domain.Repositories;
using GameShop.Domain.Context;
using Ninject;
using System.Web.Mvc;
using GameShop.Domain.Entities;
using Moq;
using System.Configuration;
using GameShop.UI.Infrastructure.Repository;
using GameShop.UI.Infrastructure.Context;
namespace GameShop.UI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelparam)
        {
            kernel = kernelparam;
            AddBindings();
        }
        public object GetService(Type servicetype)
        {
            return kernel.TryGet(servicetype);
        }
        public IEnumerable<object> GetServices(Type servicetype)
        {
            return kernel.GetAll(servicetype);
        }
        private void AddBindings()
        {
            kernel.Bind<IGameRepository>().To<EFGameRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                   .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            kernel.Bind<IAuthentication>().To<FormAuthentication>();
        }
    }
}