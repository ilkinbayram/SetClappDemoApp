using Autofac;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Microsoft.AspNetCore.Http;




namespace Core.DependencyResolvers
{
    public class ResolverCoreModuleAutofac : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            //builder.RegisterType<ClientSideStorageHelper>().As<ISessionStorageHelper>();
            //builder.RegisterType<ConfigHelper>().As<IConfigHelper>();
        }
    }
}
