using Autofac;

namespace Core.DependencyResolvers
{
    public static class CoreInstanceFactory
    {
        public static T GetInstance<T>()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ResolverCoreModuleAutofac());

            var result = builder.Build().Resolve<T>();

            return result;
        }
    }
}
