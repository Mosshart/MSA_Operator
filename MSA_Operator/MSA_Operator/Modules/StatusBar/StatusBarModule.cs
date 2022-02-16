using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace StatusBar
{
    public class StatusBarModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("StatusBar", typeof(Views.StatusBar));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}