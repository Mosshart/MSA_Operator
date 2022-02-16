using Prism.Mvvm;
using System;
using MSAEventAggregator.Core;
using Prism.Events;
using System.Windows.Threading;
using RosCommunication;
using RosCommunication.Messages.geometry_msgs;
using MSAOperator.Services;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace MovementButton.ViewModels
{
    /// <summary>
    /// View model of movement joystick button control 
    /// </summary>
    public class MovementButtonViewModel : BindableBase
    {
        private IEventAggregator _ea;
        private DispatcherTimer _timer;
        private Publisher twistPublisher;
        private RosNodeService _node;

        /// <summary>
        /// movement button constructor, starts thread to send movement data to robot
        /// </summary>
        /// <param name="ea"></param>
        /// <param name="node"></param>
        public MovementButtonViewModel(IEventAggregator ea, RosNodeService node)
        {
            _node = node;
            _ea = ea;
            _ea.GetEvent<Events>().Subscribe(MessageReceived);
            _timer = new DispatcherTimer(DispatcherPriority.Render);
           
            twistPublisher = _node.node.CreatePublisher(MessageType.Twist, "cmd_vel", "");
            _node.node.Start();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += (sender, args) =>
            {
                UpdateTimer_Tick();
            };
            _timer.Start();
        }

        private Tuple<float, float> lastSentValues;

        private void UpdateTimer_Tick()
        {
            float x = 0;
            float y = 0;

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
            
            lastSentValues = new Tuple<float, float>(x, y);
        }


        private bool _isClicked = false;

        /// <summary>
        /// Is movementbutton clicked
        /// </summary>
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

        /// <summary>
        /// Get/set button color style (image) as path string
        /// </summary>
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

        /// <summary>
        /// get/set angle of central dot 
        /// </summary>
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
        private double _visualX = 0;
        private double _visualY = 0;
        /// <summary>
        /// Big dot position X
        /// </summary>
        public double PanelX
        {
            get { return _panelX; }
            set
            {
                if (value.Equals(_panelX)) return;
                    SetProperty(ref _panelX, value);
                    VisualX = value;
                    CalculateDotPosition();
            }
        }
        /// <summary>
        /// Big dot position Y
        /// </summary>
        public double PanelY
        {
            get { return _panelY; }
            set
            {
                if (value.Equals(_panelY)) return;
                    SetProperty(ref _panelY, value);
                VisualY = value;
            }
        }
        private double _visualDotX;
        private double _visualDotY;


      
        /// <summary>
        /// Calculates if grabbed visual dot is moved outside big circle, if yes returns true
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
        /// Calculates central dot positin, if outside big circle block it on the edge.
        /// </summary>
        public void CalculateDotPosition()
        {

            double x1 = 80, y1 = 80, //punkt 1
            x2 = PanelX, y2 = PanelY, // punkt 2 
            r = 80;
            if(IsPointInsideCircle(x2,y2, r)){
                double x, y;
                double phi = Math.Atan2(y2 - y1, x2 - x1);
                y = x1 + r * Math.Sin(phi);
                x = y1 + r * Math.Cos(phi);
                VisualDotX = x;
                VisualDotY = y;
            }
            else
            {
                VisualDotX = PanelX;
                VisualDotY = PanelY;
            }
            
        }
        /// <summary>
        /// sets X postion of visual dot
        /// </summary>
        public double VisualX
        {
            get { return _visualX; }
            set
            {
                if (IsClicked == false) return;

                SetProperty(ref _visualX, value - 80);

            }
        }
        /// <summary>
        /// sets Y postion of visual dot
        /// </summary>
        public double VisualY
        {
            get { return _visualY; }
            set
            {
                if (IsClicked == false) return;
                SetProperty(ref _visualY, value - 80);
            }
        }
        /// <summary>
        /// sets X position of visual dot (duplicated from VisualX as a temp value)
        /// </summary>
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
        /// sets Y position of visual dot duplicated from VisualY as a temp value)
        /// </summary>
        public double VisualDotY
        {
            get { return _visualDotY; }
            set
            {
                if (IsClicked == false) return;
                SetProperty(ref _visualDotY, value -80);
            }
        }
     
    }
}
