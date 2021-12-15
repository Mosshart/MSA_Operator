using System;
using Prism.Commands;
using Prism.Mvvm;

namespace MSA_Operator.ViewModels.LocalizationViewModel
{
    class FavoriteButtonViewModel : BindableBase
    {
        public FavoriteButtonViewModel()
        {
            FavoriteCommand = new DelegateCommand(Execute, CanExecute);
        }

        private void Execute()
        {
            throw new NotImplementedException();
        }

        private bool CanExecute()
        {
            return true;
        }

        public DelegateCommand FavoriteCommand { get; private set; }
    }
}
