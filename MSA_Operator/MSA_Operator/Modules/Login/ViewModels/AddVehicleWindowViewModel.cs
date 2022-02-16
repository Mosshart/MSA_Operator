using Login.Business;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Runtime.InteropServices;
using System.Net;
using MSOperatorDBService;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Login.ViewModels
{
    public class AddVehicleWindowViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {

        IRegionManager _regionManager;
        private string _name;
        private string _ipAddress;
        private DatabaseModel _db;

        public DelegateCommand CancelButtonCommand { get; private set; }
        public DelegateCommand AddButtonCommand { get; private set; }

        public AddVehicleWindowViewModel(IRegionManager regionManager, DatabaseModel dbModel)
        {
            _regionManager = regionManager;
            _db = dbModel;
            CancelButtonCommand = new DelegateCommand(CancelButton);
            AddButtonCommand = new DelegateCommand(AddButton);
        }

        private void CancelButton()
        {
            var parameters = new NavigationParameters();
            parameters.Add("IsAnimation", true);
            parameters.Add("NewVehAdded", true);
            _regionManager.RequestNavigate("WindowRegion", "VehicleSelectionWindow", parameters);
        }

        private void AddButton()
        {
            var parameters = new NavigationParameters();
            parameters.Add("IsAnimation", false);
            parameters.Add("NewVehAdded", false);
            IPAddress ip;
            bool isIpValid = IPAddress.TryParse(IpAddress, out ip);
          
            if (isIpValid)
            {
                Robots r = new Robots();
                r.IPAddress = this.IpAddress.ToString();
                r.Name = this.Name; 
                //_db.AddRobotToDB(r);
                parameters.Add("VehicleEntity",r);
               
                _regionManager.RequestNavigate("WindowRegion", "VehicleSelectionWindow", parameters);
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                SetProperty(ref _ipAddress, value);
            }
        }

        public bool KeepAlive => false;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {          
            return;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {          
            return;
        }
    }
}
