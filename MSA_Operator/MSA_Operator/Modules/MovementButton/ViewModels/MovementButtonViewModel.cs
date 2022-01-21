using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using MSAEventAggregator.Core;
using Prism.Events;
using System.Windows.Threading;
using RosCommunication;
using RosCommunication.Messages.geometry_msgs;
using MSAOperator.Services;

namespace MovementButton.ViewModels
{
    public class MovementButtonViewModel : BindableBase
    {
        IEventAggregator _ea;
        private DispatcherTimer _timer;
        private RosNode node;
        Publisher twistPublisher;
        RosNodeService _node;
        public MovementButtonViewModel(IEventAggregator ea, RosNodeService node)
        {
            _node = node;
            _ea = ea;
            _ea.GetEvent<Events>().Subscribe(MessageReceived);
            _timer = new DispatcherTimer(DispatcherPriority.Render);

            //Communication with robot
            //node = new RosNode("10.2.1.91", 8878);
            //node = new RosNode("10.130.1.150", 8877);
            twistPublisher = _node.node.CreatePublisher(MessageType.Twist, "cmd_vel", "");
            _node.node.Start();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += (sender, args) =>
            {
                UpdateTimer_Tick();
            };
            _timer.Start();
        }

        Tuple<float, float> lastSentValues;
        private void UpdateTimer_Tick()
        {
            float x = 0;// (float)_visualDotX;
            float y = 0;// (float)_visualDotY;

            if(_visualDotX != 0 && _visualDotY != 0)
            {
                 x = ((float)_visualDotX * 100) / 8000;
                 y = (-1)*((float)_visualDotY * 100) / 8000;
            }
            if (lastSentValues?.Item1 == 0 && lastSentValues?.Item2 == 0 && x == 0 && y == 0)
                return;

            Twist twist = new Twist();
            twist.linear.x = y;
            twist.angular.z = -x;

            twistPublisher.Publish(twist);
            
           // System.Diagnostics.Trace.WriteLine("X: " + x + ", Y: " + y);
            lastSentValues = new Tuple<float, float>(x, y);
        }


        private bool _isClicked = false;

        public bool IsClicked
        {
            get => _isClicked;
            set
            {
                if (value == false)
                {
                    VisualX = 80;
                    VisualY = 80;
                    VisualDotX = 80;
                    VisualDotY = 80;
                }
                SetProperty(ref _isClicked, value) ;
            }
        }

        private void MessageReceived(string iconPath)
        {
            ButtonStyle = iconPath;
        } 

        private string _buttonStyle = @"../Images/Control_Dark.png";
        private int _angle;

        public string ButtonStyle
        {
            get
            {
                return _buttonStyle;
            }
            set
            {
                if (_buttonStyle == value)
                    return;
                SetProperty(ref _buttonStyle, value);
            }
        }


        public int Angle
        {
            get => _angle;
            set
            {
                SetProperty(ref _angle, value);
            }
        }

        private double _panelX;
        private double _panelY;
      /*  #region do wywalenia 
        private string _labelX;
        private string _labelY;
        #endregion*/
        private double _visualX = 0;
        private double _visualY = 0;
        public double PanelX
        {
            get { return _panelX; }
            set
            {
                if (value.Equals(_panelX)) return;
                    SetProperty(ref _panelX, value);
                   // LabelX = "X " + value.ToString();
                    VisualX = value;
                    CalculateDotPosition();
                    //_panelX = value;
                    //  OnPropertyChanged("PanelX");
            }
        }
        public double PanelY
        {
            get { return _panelY; }
            set
            {
                if (value.Equals(_panelY)) return;
                    SetProperty(ref _panelY, value);

                //LabelY = "Y " + value.ToString();
                VisualY = value;
                //_panelY = value;
                //OnPropertyChanged("PanelY");
            }
        }
        private double _visualDotX;
        private double _visualDotY;

        public double VisualX
        {
            get { return _visualX; }
            set
            {
                if (IsClicked == false) return;
               
                SetProperty(ref _visualX, value - 80);
                //_panelX = value;
                //  OnPropertyChanged("PanelX");
            }
        }
        public double VisualY
        {
            get { return _visualY; }
            set
            {
                if (IsClicked == false) return;
                SetProperty(ref _visualY, value -80);
                //_panelY = value;
                //OnPropertyChanged("PanelY");
            }
        }


        public double VisualDotX
        {
            get { return _visualDotX; }
            set
            {
                if (IsClicked == false) return;
                SetProperty(ref _visualDotX, value - 80);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="circleRadius"></param>
        /// <returns></returns>
        private bool IsPointInsideCircle(double x, double y, double circleRadius)
        {
            return !(Math.Sqrt(Math.Pow((x - 80), 2) + Math.Pow((y - 80), 2)) < circleRadius);
        }
        /// <summary>
        ///
        /// 
        /// </summary>
        public void CalculateDotPosition()
        {

            double x1 = 80, y1 = 80, //punkt 1
            x2 = PanelX, y2 = PanelY, // punkt 2 
            r = 80;

            //if (x2 > 160 || x2 <0 || y2 >160 || y2 <0) {
            if(IsPointInsideCircle(x2,y2, r)){
                double x, y;
                double phi = Math.Atan2(y2 - y1, x2 - x1);
                y = x1 + r * Math.Sin(phi);
                x = y1 + r * Math.Cos(phi);

                /*   x = PanelX + (80 * Math.Sin(phi));
                   y = PanelY + (80 * Math.Cos(phi));*/
                VisualDotX = x;
                VisualDotY = y;
             //   LabelRed = x.ToString("0.##") + "-||-" + y.ToString("0.##");
            }
            else
            {
                VisualDotX = PanelX;
                VisualDotY = PanelY;
            }
            
        }

        public double VisualDotY
        {
            get { return _visualDotY; }
            set
            {
                if (IsClicked == false) return;
                SetProperty(ref _visualDotY, value -80);
            }
        }
        /*
        public string LabelX
        {
            get => _labelX;
            set
            {
                SetProperty(ref _labelX, value);
            }
        }
        
        private string _labelRed;
        public string LabelRed
        {
            get => _labelRed;
            set
            {
                SetProperty(ref _labelRed, value);
            }
        }
        public string LabelY
        {
            get => _labelY;
            set
            {
                SetProperty(ref _labelY, value);
            }
        }*/
    }
}
