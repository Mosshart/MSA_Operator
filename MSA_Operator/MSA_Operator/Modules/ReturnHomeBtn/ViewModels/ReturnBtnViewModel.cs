
//using MSAEventAggregator.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ReturnHomeBtn.ViewModels
{
    public class ReturnBtnViewModel : BindableBase
    {
        private IEventAggregator _ea;
        public DelegateCommand ReturnHomeBtnCommand { get; private set; }
        public ReturnBtnViewModel(IEventAggregator ea)
        {
            _ea = ea;

            ReturnHomeBtnCommand = new DelegateCommand(ReturnHomeBtn);
        }

        private void ReturnHomeBtn()
        {

            //tutaj wyslanie do robota sciezki powrotu do operatora

           // _ea.GetEvent<AddPin>().Publish(true);
        }
    }
}
