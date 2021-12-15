using Login.Business;
using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Login.ViewModels
{
    public class VehicleSelectionWindowViewModel : BindableBase, INavigationAware
    {
        IRegionManager _regionManager;
        private ObservableCollection<VehicleEntity> _vehicles;
        public ObservableCollection<VehicleEntity> Vehicles
        {
            get { return _vehicles; }
            set { SetProperty(ref _vehicles, value); }
        }


        public DelegateCommand<VehicleEntity> SelectedVehicleChangeCommand { get; private set; }
        public DelegateCommand NavigateToAddVehicleCommand { get; private set; }
        public DelegateCommand SelectVehicleCommand { get; private set; }
        public VehicleSelectionWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateToAddVehicleCommand = new DelegateCommand(NavigateToAddVehicle);
            SelectVehicleCommand = new DelegateCommand(SelectVehicle);
            SelectedVehicleChangeCommand = new DelegateCommand<VehicleEntity>(SelectedVehicleChange);
            populateListBox();
        }


        private void SelectedVehicleChange(VehicleEntity obj)
        {
            foreach (VehicleEntity x in Vehicles)
            {
                x.IsChecked = false;
            }
            obj.IsChecked = true;
        }

        private void SelectVehicle()
        {
            var parameters = new NavigationParameters();
            parameters.Add("IsAnimation", true);
            _regionManager.RequestNavigate("MainRegion", "Map", parameters);
            _regionManager.RequestNavigate("MovementButtonRegion", "MovementButton");
            _regionManager.RequestNavigate("LocalizationListBtnRegion", "LocalizationListBtn");
            _regionManager.RequestNavigate("HamburgerMenuBtnRegion", "HamburgerMenuBtn");
            _regionManager.RequestNavigate("ReturnHomeBtnRegion", "ReturnBtn");
            _regionManager.RequestNavigate("LocalizenBtnRegion", "ShowOverlay");
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
            VehicleEntity vEnt =navigationContext.Parameters.GetValue<VehicleEntity>("VehicleEntity");
            return;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {           
            return;
        }

        #region testOnly To Delete
        void populateListBox()
        {

            ObservableCollection<VehicleEntity> vehicles = new ObservableCollection<VehicleEntity>();
            for (int i = 0; i <= 3; i++)
            {
                vehicles.Add(new VehicleEntity() { IPAddress = "192.168.0." + i.ToString(), Name = "Pojazd_" + i.ToString() });

            }


            Vehicles = vehicles;
        }

        #endregion
    }
}
