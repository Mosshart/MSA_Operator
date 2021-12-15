using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Localization.Business
{
    public class Location : INotifyPropertyChanged
    {
        private string _locationText;
        public string LocationText
        {
            get => _locationText;
            set
            {
                _locationText = value;
                OnPropertyChanged();
            }
        }
      /*  private string _imageSource = "../Images/Icon_Vehicle.png";
        public string ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }*/
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion //INotifyPropertyChanged
    }
}
