using System;
using Prism.Commands;
using Prism.Mvvm;

namespace MSA_Operator.ViewModels.LocalizationViewModel
{
    class LocalizationOnMapButtonViewModel : BindableBase
    {
        public LocalizationOnMapButtonViewModel()
        {
            LocalizationOnMap = new DelegateCommand(Execute, CanExecute);
        }

        private void Execute()
        {
            throw new NotImplementedException();
        }

        private bool CanExecute()
        {
            return true;
        }

        public DelegateCommand LocalizationOnMap { get; private set; }
    }
}
