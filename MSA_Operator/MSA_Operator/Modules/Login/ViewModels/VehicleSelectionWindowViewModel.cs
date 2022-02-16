using Login.Business;
using MSAOperator.Services;
using MSOperatorDBService;
using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
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
        IRegionManager _regionManager;
        private ObservableCollection<VehicleEntity> _vehicles;
        public ObservableCollection<VehicleEntity> Vehicles
        {
            get { return _vehicles; }
            set {
             
                SetProperty(ref _vehicles, value); }
        }


        public DelegateCommand<VehicleEntity> SelectedVehicleChangeCommand { get; private set; }
        public DelegateCommand NavigateToAddVehicleCommand { get; private set; }
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
            //SelectVehicleCommand = new DelegateCommand(Test);
          
            SelectedVehicleChangeCommand = new DelegateCommand<VehicleEntity>(SelectedVehicleChange);
            // populateListBox();
           // VehiclesFromDB();
        }
        private void Test()
        {
            Vehicles.Clear();
            Vehicles = null;
            foreach (VehicleEntity x in Vehicles)
            {
                x.IsChecked = false;
            }
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


        private SolidColorBrush _buttonLoginColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#707070");//("#0FEFAB"
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
            else
            {

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

      

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //Test();
            return;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //if (!navigationContext.Parameters.GetValue<bool>("NewVehAdded"))
            //{
            //    VehiclesFromDB();
            //    ButtonloginColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#707070");
            //}
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

       
        void VehiclesFromDB()
        {

            ObservableCollection<VehicleEntity> vehicles = new ObservableCollection<VehicleEntity>();

            //tutaj pobrac pojazdy z bazy danych           
           
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
