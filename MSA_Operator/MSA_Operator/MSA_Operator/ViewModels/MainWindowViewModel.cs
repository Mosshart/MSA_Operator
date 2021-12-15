using System.Linq;
using Prism.Mvvm;
using Prism.Regions;

namespace MSA_Operator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "MSA Operator";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private readonly IRegionManager _regionManager;
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        } 
    }
}
