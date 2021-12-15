using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Login.Business
{
    public class VehicleEntity : INotifyPropertyChanged
    {
        private string _name;
        private string _iPAddress;
        private bool _isChecked;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string IPAddress
        {
            get => _iPAddress;
            set
            {
                _iPAddress = value;
                OnPropertyChanged();
            }
        }
        
        public bool IsChecked
        {
            get => _isChecked;
            set
            {               
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        #endregion //INotifyPropertyChanged
    }
}
