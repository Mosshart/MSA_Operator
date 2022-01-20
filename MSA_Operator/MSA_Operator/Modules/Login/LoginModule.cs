using Login.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Login
{
    public class LoginModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainRegion", typeof(LoginOverlay));
            regionManager.RegisterViewWithRegion("WindowRegion", typeof(LoginWindow));
            regionManager.RegisterViewWithRegion("LogoutButton", typeof(LogoutButtonView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<LoginOverlay>();
            containerRegistry.RegisterForNavigation<LoginOverlay>();
            containerRegistry.RegisterForNavigation<LoginWindow>();
            containerRegistry.RegisterForNavigation<VehicleSelectionWindow>();
            containerRegistry.RegisterForNavigation<AddVehicleWindow>();
            containerRegistry.RegisterForNavigation<LogoutButtonView>();
        }
    }
}