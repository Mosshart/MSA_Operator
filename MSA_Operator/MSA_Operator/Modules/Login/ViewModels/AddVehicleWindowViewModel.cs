using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Net;
using MSOperatorDBService;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Login.ViewModels
{   
    /// <summary>
    /// View model of add vehicle control 
    /// </summary>
    public class AddVehicleWindowViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private IRegionManager _regionManager;
        private string _name;
        private string _ipAddress;
        private DatabaseModel _db;
        /// <summary>
        /// cancel button click action
        /// </summary>
        public DelegateCommand CancelButtonCommand { get; private set; }
        /// <summary>
        /// add vehicle button click action
        /// </summary>
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

        /// <summary>
        /// Textbox name 
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }

        /// <summary>
        /// Textbox ipaddress
        /// </summary>
        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                SetProperty(ref _ipAddress, value);
            }
        }
    #region interface implementation
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
    #endregion
    }
}
