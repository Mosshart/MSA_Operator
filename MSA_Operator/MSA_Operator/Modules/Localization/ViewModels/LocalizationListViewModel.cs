using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Localization.Business;
using MSAEventAggregator.Core;
using Prism.Events;
using Prism.Regions;

namespace Localization.ViewModels
{
    public class LocalizationListViewModel : BindableBase
    {
        private ObservableCollection<Location> _locations;
        public ObservableCollection<Location> Locations
        {
            get { return _locations; }
            set { SetProperty(ref _locations, value); }
        }

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _ea;
        public DelegateCommand<Location> LocationSelectedCommand { get; private set; }
        public DelegateCommand NavigateToFavoriteListCommand { get; private set; }


        public LocalizationListViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            _ea = ea;
            LocationSelectedCommand = new DelegateCommand<Location>(LocationSelected);
            NavigateToFavoriteListCommand = new DelegateCommand(NavigateToFavoriteList);

            _regionManager.RequestNavigate("LocalizationDetails", "LocalizationDetails");
            _ea.GetEvent<AddToListEvent>().Subscribe(RecivedAddToList);
            //LocationSelected();
            populateListBox();
        }

        private void NavigateToFavoriteList()
        {
            var parameters = new NavigationParameters();           

            _regionManager.RequestNavigate("LocalizationRegion", "FavoriteLocalizationList", parameters);            
        }

        private void RecivedAddToList(string obj)
        {
            if(Locations.Any(x => x.LocationText == obj) == true)
                return;
            Locations.Insert(0,new Location(){LocationText = obj});
        }

        private void LocationSelected(Location obj)
        {
            var parameters = new NavigationParameters();
            parameters.Add("location", obj);

            if (obj != null)
            {
                _regionManager.RequestNavigate("LocalizationDetails", "LocalizationDetails", parameters);
            }
        }

        //TEST ONLY
        void populateListBox(string location = "")
        {

            ObservableCollection<Location> locations = new ObservableCollection<Location>();
            for (int i = 0; i <= 20; i++)
            {
                locations.Add(new Location() { LocationText = "TEst" + i.ToString() });
            }

            if (location != "")
            {

            }
            Locations = locations;
        }
    }
}
