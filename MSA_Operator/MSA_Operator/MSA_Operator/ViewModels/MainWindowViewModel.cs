using System.Linq;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using Prism.Events;
using MSAEventAggregator.Core;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace MSA_Operator.ViewModels
{
    /// <summary>
    /// Main window classs, first view created after start
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        IEventAggregator _ea;
        private string _title = "MSA Operator";
        /// <summary>
        /// Window tittle
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// main window constructor
        /// </summary>
        /// <param name="ea">event aggregator used to exchange events between modules</param>
        public MainWindowViewModel(IEventAggregator ea)
        {            
            _ea = ea;
            _ea.GetEvent<LocalizationFindEvent>().Subscribe(HideShowForLocalization);
            _ea.GetEvent<CameraWindowEvent>().Subscribe(HideShowForCameraView);
        }

        #region visibility
        private Visibility _mainRegionVisibility = Visibility.Visible;
        /// <summary>
        /// Is mainregion visivle
        /// </summary>
        public Visibility MainRegionVisibility
        {
            get => _mainRegionVisibility;
            set
            {
                SetProperty(ref _mainRegionVisibility, value);
            }
        }

        private Visibility _statusBarVisibility = Visibility.Visible;
        /// <summary>
        /// Is Statusbar visible
        /// </summary>
        public Visibility StatusBarVisibility
        {
            get => _statusBarVisibility;
            set
            {
                SetProperty(ref _statusBarVisibility, value);
            }
        }

        private Visibility _returnHomeBtnRegionVisibility = Visibility.Visible;
        /// <summary>
        /// Is return home button visible
        /// </summary>
        public Visibility ReturnHomeBtnRegionVisibility
        {
            get => _returnHomeBtnRegionVisibility;
            set
            {
                SetProperty(ref _returnHomeBtnRegionVisibility, value);
            }
        }

        private Visibility _localizationListBtnRegionVisibility = Visibility.Visible;
        /// <summary>
        /// Is localization button visible
        /// </summary>
        public Visibility LocalizationListBtnRegionVisibility
        {
            get => _localizationListBtnRegionVisibility;
            set
            {
                SetProperty(ref _localizationListBtnRegionVisibility, value);
            }
        }

        private Visibility _hamburgerMenuBtnRegionVisibility = Visibility.Visible;
        /// <summary>
        /// ishamburger meny button visible
        /// </summary>
        public Visibility HamburgerMenuBtnRegionVisibility
        {
            get => _hamburgerMenuBtnRegionVisibility;
            set
            {
                SetProperty(ref _hamburgerMenuBtnRegionVisibility, value);
            }
        }

        private Visibility _movementButtonRegionVisibility = Visibility.Visible;
        /// <summary>
        /// is movement joystick visible
        /// </summary>
        public Visibility MovementButtonRegionVisibility
        {
            get => _movementButtonRegionVisibility;
            set
            {
                SetProperty(ref _movementButtonRegionVisibility, value);
            }
        }

        private Visibility _localizationRegionVisibility = Visibility.Visible;
        /// <summary>
        /// Visibility for localization region
        /// </summary>
        public Visibility LocalizationRegionVisibility
        {
            get => _localizationRegionVisibility;
            set
            {
                SetProperty(ref _localizationRegionVisibility, value);
            }
        }

        private Visibility _hamburgerMenuRegionVisibility = Visibility.Visible;
        /// <summary>
        /// Visibility for hamburger menu region
        /// </summary>
        public Visibility HamburgerMenuRegionVisibility
        {
            get => _hamburgerMenuRegionVisibility;
            set
            {
                SetProperty(ref _hamburgerMenuRegionVisibility, value);
            }
        }
        #endregion

        /// <summary>
        /// Hides/show icons after camera button is clicked
        /// </summary>
        /// <param name="isHidden">is hide option or unhide</param>
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
        /// <summary>
        /// Hides/show icons after localization button is clicked
        /// </summary>
        /// <param name="isHidden">is hide option or unhide</param>
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
