using MSA_Operator.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Prism.Unity;
using MSAOperator.Services;
using System.Device.Location;
using MSAOperator.Services.BatteryService.Operator;
using MSAOperator.Services.BatteryService.Robot;
using MSOperatorDBService;

namespace MSA_Operator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<RosNodeService>();
            containerRegistry.RegisterSingleton<GeoLocalizationService>();
            containerRegistry.RegisterSingleton<WLanInfoService>();
            containerRegistry.Register<DatabaseModel>();

        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {          
            moduleCatalog.AddModule<StatusBar.StatusBarModule>();
            moduleCatalog.AddModule<MovementButton.MovementButtonModule>();
            moduleCatalog.AddModule<Map.MapModule>(); 
            moduleCatalog.AddModule<Camera.CameraModule>();
            moduleCatalog.AddModule<Localize.LocalizeModule>();
            moduleCatalog.AddModule<Localization.LocalizationModule>();
            moduleCatalog.AddModule<HamburgerMenu.HamburgerMenuModule>();
            moduleCatalog.AddModule<ReturnHomeBtn.ReturnHomeBtnModule>();
            moduleCatalog.AddModule<Login.LoginModule>();
        }
    }
}
