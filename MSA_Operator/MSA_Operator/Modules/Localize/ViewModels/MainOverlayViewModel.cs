using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MSAEventAggregator.Core;
using Prism.Events;
using Prism.Regions;

namespace Localize.ViewModels
{
    public class MainOverlayViewModel : BindableBase, INavigationAware
    {
        public DelegateCommand CloseCommand { get; private set; }
        private readonly IRegionManager _regionManager;

        public DelegateCommand LocalizeOperator { get; private set; }
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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            bool isAnimation = (bool)navigationContext.Parameters["IsAnimation"];
            if (isAnimation != null)
                IsAnimationRunning = isAnimation;
        }

       
        private bool _isAnimationRunning = false;

        public bool IsAnimationRunning
        {
            get => _isAnimationRunning;
            set { SetProperty(ref _isAnimationRunning, value); }
        }

    }
}
 