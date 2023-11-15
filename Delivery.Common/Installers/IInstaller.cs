using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Common.Installers
{
    public interface IInstaller
    {
        void Install(IServiceCollection serviceCollection);
    }
}