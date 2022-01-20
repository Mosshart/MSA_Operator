using System.Linq;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using Prism.Events;
using MSAEventAggregator.Core;

namespace MSA_Operator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IEventAggregator _ea;
        private string _title = "MSA Operator";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private readonly IRegionManager _regionManager;
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager; 
            _ea = ea;
            _ea.GetEvent<LocalizationFindEvent>().Subscribe(HideShowForLocalization);
            _ea.GetEvent<CameraWindowEvent>().Subscribe(HideShowForCameraView);
        }

        #region visibility
        private Visibility _mainRegionVisibility = Visibility.Visible;
        public Visibility MainRegionVisibility
        {
            get => _mainRegionVisibility;
            set
            {
                SetProperty(ref _mainRegionVisibility, value);
            }
        }

        private Visibility _statusBarVisibility = Visibility.Visible;
        public Visibility StatusBarVisibility
        {
            get => _statusBarVisibility;
            set
            {
                SetProperty(ref _statusBarVisibility, value);
            }
        }

        private Visibility _returnHomeBtnRegionVisibility = Visibility.Visible;
        public Visibility ReturnHomeBtnRegionVisibility
        {
            get => _returnHomeBtnRegionVisibility;
            set
            {
                SetProperty(ref _returnHomeBtnRegionVisibility, value);
            }
        }

        private Visibility _localizationListBtnRegionVisibility = Visibility.Visible;
        public Visibility LocalizationListBtnRegionVisibility
        {
            get => _localizationListBtnRegionVisibility;
            set
            {
                SetProperty(ref _localizationListBtnRegionVisibility, value);
            }
        }

        private Visibility _hamburgerMenuBtnRegionVisibility = Visibility.Visible;
        public Visibility HamburgerMenuBtnRegionVisibility
        {
            get => _hamburgerMenuBtnRegionVisibility;
            set
            {
                SetProperty(ref _hamburgerMenuBtnRegionVisibility, value);
            }
        }

        private Visibility _movementButtonRegionVisibility = Visibility.Visible;
        public Visibility MovementButtonRegionVisibility
        {
            get => _movementButtonRegionVisibility;
            set
            {
                SetProperty(ref _movementButtonRegionVisibility, value);
            }
        }

        private Visibility _localizationRegionVisibility = Visibility.Visible;
        public Visibility LocalizationRegionVisibility
        {
            get => _localizationRegionVisibility;
            set
            {
                SetProperty(ref _localizationRegionVisibility, value);
            }
        }

        private Visibility _hamburgerMenuRegionVisibility = Visibility.Visible;
        public Visibility HamburgerMenuRegionVisibility
        {
            get => _hamburgerMenuRegionVisibility;
            set
            {
                SetProperty(ref _hamburgerMenuRegionVisibility, value);
            }
        }
        #endregion

        private void HideShowForCameraView(bool isHidden)
        {
            if (isHidden)
            {
                LocalizationListBtnRegionVisibility = Visibility.Collapsed;
                HamburgerMenuRegionVisibility = Visibility.Visible;
                ReturnHomeBtnRegionVisibility = Visibility.Collapsed;
                LocalizationListBtnRegionVisibility = Visibility.Collapsed;
            }
            else
            {
                LocalizationListBtnRegionVisibility = Visibility.Visible;
                HamburgerMenuRegionVisibility = Visibility.Collapsed;
                ReturnHomeBtnRegionVisibility = Visibility.Visible;
                LocalizationListBtnRegionVisibility = Visibility.Visible;
            }
           

        }
        private void HideShowForLocalization(bool isHidden)
        {
            Visibility status;
            if (isHidden)
            {
                status = Visibility.Collapsed;

            }
            else
            {
                status = Visibility.Visible;
            }
            LocalizationListBtnRegionVisibility = status;
            HamburgerMenuBtnRegionVisibility = status;
            ReturnHomeBtnRegionVisibility = status;
            MovementButtonRegionVisibility = status;
            LocalizationListBtnRegionVisibility = status;
        }
    }
}
