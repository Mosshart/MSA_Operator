using MSA_Operator.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Prism.Unity;
using MSAOperator.Services;
using System.Device.Location;
using MSAOperator.Services.BatteryService.Operator;
using MSAOperator.Services.BatteryService.Robot;

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
            //containerRegistry.RegisterSingleton<OperatorBatteryInfoService>();
            //containerRegistry.RegisterSingleton<OperatorBatteryInfoService>();
            //containerRegistry.RegisterSingleton<OperatorBatteryInfoService>();


            containerRegistry.Register<RosNodeService>();
            containerRegistry.RegisterSingleton<GeoLocalizationService>();
            containerRegistry.RegisterSingleton<WLanInfoService>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //status bar modules
            //  moduleCatalog.AddModule<StatusBar.StatusBarModule>();
            moduleCatalog.AddModule<StatusBar.StatusBarModule>();
            moduleCatalog.AddModule<MovementButton.MovementButtonModule>();
            moduleCatalog.AddModule<Map.MapModule>(); 
            moduleCatalog.AddModule<Camera.CameraModule>();
            moduleCatalog.AddModule<Localize.LocalizeModule>();
            moduleCatalog.AddModule<Localization.LocalizationModule>();
            moduleCatalog.AddModule<HamburgerMenu.HamburgerMenuModule>();
            moduleCatalog.AddModule<ReturnHomeBtn.ReturnHomeBtnModule>();
            moduleCatalog.AddModule<Login.LoginModule>();
            // moduleCatalog.AddModule<Battery.BatteryModule>();
        }
    }
}
