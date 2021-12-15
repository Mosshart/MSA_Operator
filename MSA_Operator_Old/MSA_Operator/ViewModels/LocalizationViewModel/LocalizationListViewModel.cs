using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Prism.Mvvm;

namespace MSA_Operator.ViewModels.LocalizationViewModel
{
    class LocalizationListViewModel : BindableBase
    {

        public ICollectionView LocalizationParams { get; private set; }

        public LocalizationListViewModel()
        {
            // Initialize the CollectionView for the underlying model
            // and track the current selection.
            ObservableCollection<LocalizationParam> localizationParams = new ObservableCollection<LocalizationParam>();
            LocalizationParams = new ListCollectionView(localizationParams);

            LocalizationParams.CurrentChanged += SelectedItemChanged;
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
            localizationParams.Add(new LocalizationParam());
        }

        private void SelectedItemChanged(object sender, EventArgs e)
        {
            LocalizationParam current = LocalizationParams.CurrentItem as LocalizationParam;
        }      
    }
    /*
  */
}
