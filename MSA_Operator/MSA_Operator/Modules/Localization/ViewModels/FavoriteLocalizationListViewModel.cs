using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Localization.ViewModels
{
    public class FavoriteLocalizationListViewModel : BindableBase, INavigationAware
    {
        public DelegateCommand NavigateBackCommand { get; private set; }
        public DelegateCommand<string> SetArrowPositionCommand { get; private set; }
        private readonly IRegionManager _regionManager;
     

        public FavoriteLocalizationListViewModel(IRegionManager regionManager)
        {
            NavigateBackCommand = new DelegateCommand(NavigateBack);
            SetArrowPositionCommand = new DelegateCommand<string>(SetArrowPosition);
            _regionManager = regionManager;
        }

        private void SetArrowPosition(string a)
        {
            switch (a) {
                case "1":
                    Visibility1 = Visibility.Visible;
                    Visibility2 = Visibility.Hidden;
                    Visibility3 = Visibility.Hidden;
                    break;
                case "2":
                    Visibility1 = Visibility.Hidden;
                    Visibility2 = Visibility.Visible;
                    Visibility3 = Visibility.Hidden;
                    break;
                case "3":
                    Visibility1 = Visibility.Hidden;
                    Visibility2 = Visibility.Hidden;
                    Visibility3 = Visibility.Visible;
                    break;
                default:
                    Visibility1 = Visibility.Visible;
                    Visibility2 = Visibility.Hidden;
                    Visibility3 = Visibility.Hidden;
                    break;
            }

            if (ArrowDirection == 0)
                ArrowDirection = 180;
            else
                ArrowDirection = 0;
        }

        private void NavigateBack()
        {
            _regionManager.RequestNavigate("LocalizationRegion", "LocalizationList");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
        private int _arrowDirection = 0;
        private Visibility _visibility2 = Visibility.Hidden;
        private Visibility _visibility1 = Visibility.Hidden;
        private Visibility _visibility3 = Visibility.Visible;

        public int ArrowDirection
        {
            get => _arrowDirection;
            set
            {
                SetProperty(ref _arrowDirection, value);
            }
        }
        public Visibility Visibility1
        {
            get => _visibility1;
            set
            {
                SetProperty(ref _visibility1, value);
            }
        }
        public  Visibility Visibility2
        {
            get => _visibility2;
            set
            {
                SetProperty(ref _visibility2, value);                
            }
        }
        public Visibility Visibility3
        {
            get => _visibility3;
            set
            {
                SetProperty(ref _visibility3, value);
            }
        }
    }
}
