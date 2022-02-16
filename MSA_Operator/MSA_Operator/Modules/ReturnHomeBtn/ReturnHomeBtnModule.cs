using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ReturnHomeBtn
{
    public class ReturnHomeBtnModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.ReturnBtn>();
        }
    }
}