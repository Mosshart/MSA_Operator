using Localization.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Localization
{
    public class LocalizationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
           // regionManager.RegisterViewWithRegion("LocalizationListBtn", typeof(LocalizationListBtn));
            regionManager.RegisterViewWithRegion("LocalizationDetails", typeof(LocalizationDetails));
            //regionManager.RegisterViewWithRegion("LocalizationRegion", typeof(Views.LocalizationList));
        }
        

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LocalizationListBtn>();
            containerRegistry.RegisterForNavigation<LocalizationDetails>();
            containerRegistry.RegisterForNavigation<Views.LocalizationList>();
            containerRegistry.RegisterForNavigation<Views.FavoriteLocalizationList>();
        }
        
    }
}