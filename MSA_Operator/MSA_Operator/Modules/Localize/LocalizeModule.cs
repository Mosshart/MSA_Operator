using Localize.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Localize
{
    public class LocalizeModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            //regionManager.RegisterViewWithRegion("LocalizeBtn", typeof(Views.ShowOverlay));
            //regionManager.RegisterViewWithRegion("LocalizeOverlay", typeof(Views.MainOverlay));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainOverlay>();
            containerRegistry.RegisterForNavigation<ShowOverlay>();
        }
    }
}
