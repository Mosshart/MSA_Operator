using System;
using System.Windows.Threading;
using MSA_Operator.Views;
using Prism.Mvvm;

namespace MSA_Operator.ViewModels
{
    class BatteryLevelViewModel :BindableBase
    {
        private DispatcherTimer _timer;
      
        public BatteryLevelViewModel()
        {
            ModelType a = ModelType.Operator;


            if (a == ModelType.Robot)
                BatteryPercent = 55;
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, args) =>
            {
              
                if (BatteryPercent <= 0)
                    BatteryPercent = 101;
                BatteryPercent = BatteryPercent -1;
                BatteryPercentText = BatteryPercent.ToString() + "%";
            };
            _timer.Start();
        }
        public void testDzialania1()
        {
            BatteryPercent = 15;
        }
        public void testDzialania2()
        {
            BatteryPercent = 55;
        }
        /*private ModelType _modelType;
        public ModelType ModelType
        {
            get { return _modelType; }
            set {
                if(value == ModelType.Robot)
                {
                    testDzialania1();
                }
                else
                {
                    testDzialania2();
                }
                _modelType = value; }
        }
        */
        private string _batteryPercentText = "100%";
        public string BatteryPercentText
        {
            get { return _batteryPercent.ToString() + "%"; }
            set { SetProperty(ref _batteryPercentText, value); }
        }

        private int _batteryPercent = 5;
        public int BatteryPercent
        {
            get { return _batteryPercent; }
            set { SetProperty(ref _batteryPercent, value); }
        }
    }
}
