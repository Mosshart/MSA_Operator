using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MSAEventAggregator.Core;
using Prism.Events;
using Prism.Regions;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Localize.ViewModels
{
    /// <summary>
    /// View model of localize control 
    /// </summary>
    public class MainOverlayViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// on close view button click action
        /// </summary>
        public DelegateCommand CloseCommand { get; private set; }
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// on operator localization button click action
        /// </summary>
        public DelegateCommand LocalizeOperator { get; private set; }
        /// <summary>
        /// on robot localization button click action
        /// </summary>
        public DelegateCommand LocalizeRobot { get; private set; }
        
        private void OnExecuteCloseCommand()
        {
            var singleView = _regionManager.Regions["LocalizeOverlay"].ActiveViews.FirstOrDefault();
            _regionManager.Regions["LocalizeOverlay"].Remove(singleView);
        }

        private IEventAggregator _ea;

        public MainOverlayViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            CloseCommand = new DelegateCommand(OnExecuteCloseCommand);
            _ea = ea;
            LocalizeOperator = new DelegateCommand(OnOperatorLocalizeCommand);
            LocalizeRobot = new DelegateCommand(OnRobotLocalizeCommand);
        }

        private void OnOperatorLocalizeCommand()
        {
            _ea.GetEvent<LocalizeEvent>().Publish(false);
            OnExecuteCloseCommand();
        }
        private void OnRobotLocalizeCommand()
        {
            _ea.GetEvent<LocalizeEvent>().Publish(true);
            OnExecuteCloseCommand();
        }
        #region interface implementation
        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// on navigated to this view, do animation
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            bool isAnimation = (bool)navigationContext.Parameters["IsAnimation"];
            if (isAnimation != null)
                IsAnimationRunning = isAnimation;
        }
        #endregion

        private bool _isAnimationRunning = false;
        /// <summary>
        /// Is animation on/off
        /// </summary>
        public bool IsAnimationRunning
        {
            get => _isAnimationRunning;
            set { SetProperty(ref _isAnimationRunning, value); }
        }
    }
}
 