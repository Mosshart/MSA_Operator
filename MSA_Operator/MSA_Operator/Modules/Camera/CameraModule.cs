using Camera.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Camera
{
    public class CameraModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("CameraMinimized", typeof(Views.CameraMinimalized));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Camera>();
        }
    }
}