using Login.Business;
using MSAOperator.Services;
using MSOperatorDBService;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Login.ViewModels
{
    public class VehicleSelectionWindowViewModel : BindableBase, INavigationAware
    {
        private IRegionManager _regionManager;
        private ObservableCollection<VehicleEntity> _vehicles;
        /// <summary>
        /// Vehicles list
        /// </summary>
        public ObservableCollection<VehicleEntity> Vehicles
        {
            get { return _vehicles; }
            set 
            {             
                SetProperty(ref _vehicles, value); 
            }
        }

        /// <summary>
        /// On selected vehicle change action
        /// </summary>
        public DelegateCommand<VehicleEntity> SelectedVehicleChangeCommand { get; private set; }
        /// <summary>
        /// On add vehicle button click action
        /// </summary>
        public DelegateCommand NavigateToAddVehicleCommand { get; private set; }
        /// <summary>
        /// On confim button click action
        /// </summary>
        public DelegateCommand SelectVehicleCommand { get; private set; }

        private DatabaseModel _db;
        private RosNodeService _node;
        public VehicleSelectionWindowViewModel(IRegionManager regionManager, DatabaseModel dbModel, RosNodeService node)
        {
            _regionManager = regionManager;
            _db = dbModel;
            _node = node;
            NavigateToAddVehicleCommand = new DelegateCommand(NavigateToAddVehicle);
            SelectVehicleCommand = new DelegateCommand(SelectVehicle);
          
            SelectedVehicleChangeCommand = new DelegateCommand<VehicleEntity>(SelectedVehicleChange);
        }
     
        private void SelectedVehicleChange(VehicleEntity obj)
        {
            if (obj.IsChecked == true) {
                obj.IsChecked = false;

                ButtonloginColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#707070");
                return;
            }

            foreach (VehicleEntity x in Vehicles)
            {
                x.IsChecked = false;
            }
            ButtonloginColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#0FEFAB");
            obj.IsChecked = true;
        }

        private SolidColorBrush _buttonLoginColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#707070");
        /// <summary>
        /// Button login color
        /// </summary>
        public SolidColorBrush ButtonloginColor
        {
            get => _buttonLoginColor;
            set
            {
                SetProperty(ref _buttonLoginColor, value);
            }
        }

        private void SelectVehicle()
        {
            if (Vehicles.Count(x => x.IsChecked == true) > 0)
            {
                SetNodeParameters();
                var parameters = new NavigationParameters();
                parameters.Add("IsAnimation", true);
                _regionManager.RequestNavigate("MainRegion", "Map", parameters);
                _regionManager.RequestNavigate("MovementButtonRegion", "MovementButton");
                _regionManager.RequestNavigate("LocalizationListBtnRegion", "LocalizationListBtn");
                _regionManager.RequestNavigate("HamburgerMenuBtnRegion", "HamburgerMenuBtn");
                _regionManager.RequestNavigate("ReturnHomeBtnRegion", "ReturnBtn");
                _regionManager.RequestNavigate("LocalizenBtnRegion", "ShowOverlay");
            }
        }

        private void SetNodeParameters()
        {
            string ip = Vehicles.Where(x => x.IsChecked == true).FirstOrDefault().IPAddress;
           _node.ChangeNodeConnected(ip);
        }

        private void NavigateToAddVehicle()
        {
            var parameters = new NavigationParameters();
            parameters.Add("IsAnimation", true);
            _regionManager.RequestNavigate("WindowRegion", "AddVehicleWindow", parameters);            
        }


        #region interface implementation
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            return;
        }
        /// <summary>
        /// Occurs when navigated from other view.
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (Vehicles == null)
                VehiclesFromDB();
            Robots r = navigationContext.Parameters.GetValue<Robots>("VehicleEntity");
            if (r != null)
            {
                _db.AddRobotToDB(r);
                foreach(var x in Vehicles)
                {
                    x.IsChecked = false;
                }
                Vehicles.Add(new VehicleEntity() { IPAddress = r.IPAddress, Name = r.Name });
            }

            return;
        }

        #endregion
        private void VehiclesFromDB()
        {

            ObservableCollection<VehicleEntity> vehicles = new ObservableCollection<VehicleEntity>();

            foreach (Robots veh in _db.GetAllRobots())
            {
                vehicles.Add(MapDBObjToVehicleEntity(veh));

            }
            Vehicles = vehicles;
        }

        private VehicleEntity MapDBObjToVehicleEntity(Robots robot)
        {
            VehicleEntity output = new VehicleEntity();

            output.Name = robot.Name;
            output.IPAddress = robot.IPAddress;
            output.IsChecked = false;
            return output;
        }      
    }
}