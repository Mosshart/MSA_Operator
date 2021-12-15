using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using MSA_Operator.ViewModels.StatusBarViewModel;

namespace MSA_Operator.Model
{
    class ClockModel
    {
        public DispatcherTimer _timer;
        public ClockViewModel viewModel;
        public ClockModel(ClockViewModel viewModel)
        {
            this.viewModel = viewModel;
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, args) =>
            {
                viewModel.CurrentTime = DateTime.Now.ToLongTimeString();
            };
            _timer.Start();
        }
    }
}
