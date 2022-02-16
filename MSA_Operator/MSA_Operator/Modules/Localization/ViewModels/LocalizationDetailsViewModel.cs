using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Localization.Business;
using MSAEventAggregator.Core;
using Prism.Events;
using Prism.Regions;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Localization.ViewModels
{
    /// <summary>
    /// view model LocalizationDetails  region
    /// </summary>
    public class LocalizationDetailsViewModel : BindableBase, INavigationAware
    {
        private string _currentLocalizationText= "50.328332, 18.674070";
        private string _destinationText;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _ea;

        /// <summary>
        /// Return button click action
        /// </summary>
        public DelegateCommand ReturnFromDetails { get; private set; }
        /// <summary>
        /// add cordinates to list click actions
        /// </summary>
      //  public DelegateCommand AddCordinatesToListCommand { get; private set; }


        public LocalizationDetailsViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            ReturnFromDetails = new DelegateCommand(OnReturnCommand);
          //  AddCordinatesToListCommand = new DelegateCommand(OnAddCordinatesToList);
           // AddCordinatesToListCommand = new DelegateCommand(AddPinToMap);
            _ea = ea;
            _ea.GetEvent<CenterLocationChange>().Subscribe(GetMidLocalization);
            _ea.GetEvent<CloseLocalizationDetails>().Subscribe(OnReturnCommand);

        }

        private void GetMidLocalization(string obj)
        {
            DestinationText = obj;
        }

       /// <summary>
       /// 
       /// </summary>
        //public void OnAddCordinatesToList()
        //{
        //    string coordinates = "";
        //    coordinates = ValidateCoordinates(DestinationText);

        //    _ea.GetEvent<AddToListEvent>().Publish(coordinates);

        //}

        private string ValidateCoordinates(string coordString)
        {
           return coordString;
        }



        private void OnReturnCommand()
        {
            var singleView = _regionManager.Regions["LocalizationRegion"].ActiveViews.FirstOrDefault();
            _regionManager.Regions["LocalizationRegion"].Deactivate(singleView);

            _ea.GetEvent<LocalizationFindEvent>().Publish(false);           
        }
        /// <summary>
        /// get/set current localization text 
        /// </summary>
        public string CurrentLocalizationText
        {
            get => _currentLocalizationText;
            set
            {
                SetProperty(ref _currentLocalizationText, value);
            }
        }

        /// <summary>
        /// get/set destination text 
        /// </summary>
        public string DestinationText
        {
            get => _destinationText;
            set
            {
                SetProperty(ref _destinationText, value);
            }
        }


        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var location = navigationContext.Parameters["location"] as Location;
            if (location != null)
                DestinationText = location.LocationText;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var location = navigationContext.Parameters["location"] as Location;
            if (location != null)
                return DestinationText != null && DestinationText == location.LocationText;
            else
                return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
