using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace StatusBar.ViewModels
{
    public class ClockViewModel : BindableBase
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

        
        public DispatcherTimer _timer;

        public ClockViewModel()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, args) =>
            {
                CurrentTime = DateTime.Now.ToLongTimeString();
            };
            _timer.Start();
        }
    }
}
