
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace HamburgerMenu
{
    public class HamburgerMenuModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            //regionManager.RegisterViewWithRegion("HamburgerMenuBtn", typeof(Views.HamburgerMenuBtn));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        { 
            containerRegistry.RegisterForNavigation<Views.HamburgerMenuBtn>();
            containerRegistry.RegisterForNavigation<Views.HamburgerMenu>();
            containerRegistry.RegisterForNavigation<Views.LogoutPopup>();
        }  
    }
}