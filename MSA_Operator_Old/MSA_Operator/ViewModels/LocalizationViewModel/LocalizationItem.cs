using Prism.Mvvm;

namespace MSA_Operator.ViewModels.LocalizationViewModel
{
    public class LocalizationItem : BindableBase, ILocalizationInterface
    {
        private string _itemText;
        public string ItemText
        {
            get { return _itemText; }
            set { SetProperty(ref _itemText, value); }
        }

        private string _itemImagePath = @"/Images/Localization_Icons/Icon_Last.png;";
        public string ItemImagePath
        {
            get { return _itemImagePath; }
            set { SetProperty(ref _itemImagePath, value); }
        }

    }
    public interface ILocalizationInterface
    {
        string ItemText { get; set; }
        string ItemImagePath { get; set; }
    }

}
