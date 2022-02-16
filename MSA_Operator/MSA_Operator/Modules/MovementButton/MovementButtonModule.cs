using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MovementButton
{
    public class MovementButtonModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            
        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.MovementButton>();
        }
    }
}