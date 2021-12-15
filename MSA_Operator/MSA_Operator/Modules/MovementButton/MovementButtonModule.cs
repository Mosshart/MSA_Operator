using MovementButton.ViewModels;
using MovementButton.Views;
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
            //regionManager.RegisterViewWithRegion("MovementButton", typeof(Views.MovementButton));
            
        }

        void setMovementButtonColor(Views.MovementButton mb, string style)
        {
            (mb.DataContext as MovementButtonViewModel).ButtonStyle = style;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.MovementButton>();
        }
    }
}