using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Localization.Business;
using MSAEventAggregator.Core;
using Prism.Events;
using Prism.Regions;

namespace Localization.ViewModels
{
    public class LocalizationDetailsViewModel : BindableBase, INavigationAware
    {
        private string _currentLocalizationText= "50.328332, 18.674070";
        private string _destinationText;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _ea;

        public DelegateCommand ReturnFromDetails { get; private set; }
        public DelegateCommand AddCordinatesToListCommand { get; private set; }


        public LocalizationDetailsViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            ReturnFromDetails = new DelegateCommand(OnReturnCommand);
          //  AddCordinatesToListCommand = new DelegateCommand(OnAddCordinatesToList);
            AddCordinatesToListCommand = new DelegateCommand(AddPinToMap);
            _ea = ea;
            _ea.GetEvent<CenterLocationChange>().Subscribe(GetMidLocalization);
            _ea.GetEvent<CloseLocalizationDetails>().Subscribe(OnReturnCommand);

        }

        private void GetMidLocalization(string obj)
        {
            DestinationText = obj;
        }

        private void AddPinToMap()
        {
            _ea.GetEvent<AddPin>().Publish(true);
        }

        public void OnAddCordinatesToList()
        {
            string coordinates = "";
            coordinates = ValidateCoordinates(DestinationText);

            _ea.GetEvent<AddToListEvent>().Publish(coordinates);

        }

        private string ValidateCoordinates(string coordString)
        {
           /* string value1 = coordString.Split(',')[0];
            string value2 = coordString.Split(',')[1];
            double temp;
            bool isPossible = Double.TryParse(value1, out temp);
            bool isPossible2 = Double.TryParse(value2, out temp);

            return isPossible2 && isPossible2 ? coordString : "error";*/
           return coordString;

        }



        private void OnReturnCommand()
        {
            var singleView = _regionManager.Regions["LocalizationRegion"].ActiveViews.FirstOrDefault();
            _regionManager.Regions["LocalizationRegion"].Deactivate(singleView);

            _ea.GetEvent<LocalizationFindEvent>().Publish(false);
            /* bool a = _regionManager.Regions["LocalizationDetails"].NavigationService.Journal.CanGoBack;// Remove(singleView);
             if(a == true)/
                 _regionManager.Regions["LocalizationDetails"].NavigationService.Journal.GoBack();*/
        }

        public string CurrentLocalizationText
        {
            get => _currentLocalizationText;
            set
            {
                SetProperty(ref _currentLocalizationText, value);
            }
        }

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
