using System;
using System.Windows.Threading;
using MSA_Operator.Model;
using Prism.Mvvm;

namespace MSA_Operator.ViewModels.StatusBarViewModel
{
    class ClockViewModel : BindableBase
    {
        private string _currentTime;
        public string CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                if (_currentTime == value)
                    return;
                SetProperty(ref _currentTime, value);                
            }
        }

        private ClockModel model;
        public DispatcherTimer _timer;

        public ClockViewModel()
        {
            this.model = new ClockModel(this);
         /*   _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, args) =>
            {
                CurrentTime = DateTime.Now.ToLongTimeString();
            };
            _timer.Start();*/
        }
    }
}
 