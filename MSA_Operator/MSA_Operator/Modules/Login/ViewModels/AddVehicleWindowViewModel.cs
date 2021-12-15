using Login.Business;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Runtime.InteropServices;
using System.Net;

namespace Login.ViewModels
{
    public class AddVehicleWindowViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {

        IRegionManager _regionManager;
        private string _name;
        private string _ipAddress;

        public DelegateCommand CancelButtonCommand { get; private set; }
        public DelegateCommand AddButtonCommand { get; private set; }

        public AddVehicleWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            CancelButtonCommand = new DelegateCommand(CancelButton);
            AddButtonCommand = new DelegateCommand(AddButton);
        }

        private void CancelButton()
        {
            var parameters = new NavigationParameters();
            parameters.Add("IsAnimation", true);
            _regionManager.RequestNavigate("WindowRegion", "VehicleSelectionWindow", parameters);
        }

        private void AddButton()
        {
            var parameters = new NavigationParameters();
            parameters.Add("IsAnimation", false);

            IPAddress ip;
            bool isIpValid = IPAddress.TryParse(IpAddress, out ip);
            if (isIpValid)
            {
                parameters.Add("VehicleEntity", new VehicleEntity() { Name = this.Name, IPAddress = this.IpAddress});
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
