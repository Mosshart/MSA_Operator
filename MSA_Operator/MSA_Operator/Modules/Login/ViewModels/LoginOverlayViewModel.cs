using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Login.ViewModels
{
    public class LoginOverlayViewModel : BindableBase, INavigationAware
    {
        public LoginOverlayViewModel()
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            string test = "A";
            if (test == "A")
                return;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string test = "A";
            if (test == "A")
                return;
        }
    }
}
