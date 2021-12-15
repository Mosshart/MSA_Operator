using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MSA_Operator.Views;
using Prism.Mvvm;

namespace MSA_Operator.ViewModels.StatusBarViewModel
{
    public class StatusBarViewModel:BindableBase
    {
        private int _modeOperator = 0;
        public int ModeOperator
        {
            get
            {
                return _modeOperator;
            }
            set
            {
                SetProperty(ref _modeOperator, value);
            }
        }

        private int _modeRobot = 1;
        public int ModeRobot
        {
            get
            {
                return _modeRobot;
            }
            set
            {
                SetProperty(ref _modeRobot, value);
            }
        }
    }
}
