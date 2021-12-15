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

            //regionManager.RequestNavigate("LoginWindow", "LoginWindow");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<LoginOverlay>();
            containerRegistry.RegisterForNavigation<VehicleSelectionWindow>();
            containerRegistry.RegisterForNavigation<AddVehicleWindow>();
        }
    }
}