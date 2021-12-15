using System;
using Prism.Commands;
using Prism.Mvvm;

namespace MSA_Operator.ViewModels.LocalizationViewModel
{
    class LocalizationParam : BindableBase
    {
        public LocalizationParam() 
        {
            onLocalizationButton = new DelegateCommand(Execute, CanExecute);
        }

    

        public DelegateCommand onLocalizationButton { get; private set; }
        private void Execute()
        {
            throw new NotImplementedException();
        }

        private bool CanExecute()
        {
            return true;
        }

        private string _localizationText = "50.329024, 18.672615";
        public string LocalizationText
        {
            get
            {
                return _localizationText;
            }
            set
            {
                SetProperty(ref _localizationText, value);
            }
        }
         
    }
}
