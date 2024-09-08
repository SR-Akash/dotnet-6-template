using Autofac;
using MGM_Lite.IRepository;
using MGM_Lite.Repository;
using MGM_Lite.Repository.Auth;

namespace MGM_Lite.Configurations
{
    public class DependencyContainer
    {
        internal static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<Login>().As<ILogin>();
            builder.RegisterType<ConfigurationModule>().As<IConfigurationModule>();
            builder.RegisterType<Purchase>().As<IPurchase>();
        }
    }
}
