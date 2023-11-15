﻿using Delivery.Common;
using Delivery.Common.Installers;
using Delivery.Web.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Web.DAL.Installers
{
    public class WebDALInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<LocalDb>();
            serviceCollection.Scan(scan =>
                scan.FromAssemblyOf<WebDALInstaller>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IWebRepository<,,>)))
                    .AsSelf()
                    .WithSingletonLifetime());
        }
    }
}
