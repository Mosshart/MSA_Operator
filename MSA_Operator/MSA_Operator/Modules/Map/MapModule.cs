using Map.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Map
{
    public class MapModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
          //  regionManager.RegisterViewWithRegion("MainRegion", typeof(Views.Map));
            regionManager.RegisterViewWithRegion("MapMinimalized", typeof(Views.MapMinimalized));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Map>("Map");
        }
    }
}