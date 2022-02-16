using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace MovementButton.Behaviours
{
    class MouseBehaviour : Behavior<Panel>
    {
        public static readonly DependencyProperty MouseYProperty = DependencyProperty.Register(
            "MouseY", typeof(double), typeof(MouseBehaviour), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty MouseXProperty = DependencyProperty.Register(
            "MouseX", typeof(double), typeof(MouseBehaviour), new PropertyMetadata(default(double)));

        public double MouseY
        {
            get { return (double)GetValue(MouseYProperty); }
            set { SetValue(MouseYProperty, value); }
        }

        public double MouseX
        {
            get { return (double)GetValue(MouseXProperty); }
            set { SetValue(MouseXProperty, value); }
        }


        public static readonly DependencyProperty MouseLeftButtonClickedProperty = DependencyProperty.Register(
            "MouseLeftButtonClicked", typeof(bool), typeof(MouseBehaviour), new PropertyMetadata(default(bool)));

        public bool MouseLeftButtonClicked
        {
            get { return (bool)GetValue(MouseLeftButtonClickedProperty); }
            set { SetValue(MouseLeftButtonClickedProperty, value); }
        }

        public static readonly DependencyProperty MouseLeftButtonReleasedProperty = DependencyProperty.Register(
            "MouseLeftButtonReleased", typeof(bool), typeof(MouseBehaviour), new PropertyMetadata(default(bool)));

        public bool MouseLeftButtonReleased
        {
            get { return (bool)GetValue(MouseLeftButtonReleasedProperty); }
            set { SetValue(MouseLeftButtonReleasedProperty, value); }
        }



        protected override void OnAttached()
        {
            AssociatedObject.MouseMove += AssociatedObjectOnMouseMove;
            AssociatedObject.MouseLeftButtonDown += onLeftMouseButtonClicked;
            AssociatedObject.MouseLeftButtonUp += onLeftMouseButtonReleased;
           // AssociatedObject.LostMouseCapture += onLeftMouseButtonReleased;
        }

        private void onLeftMouseButtonClicked(object sender, MouseButtonEventArgs e)
        {
            MouseLeftButtonClicked = true;
        }

        private void onLeftMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            var pos = e.ButtonState == MouseButtonState.Pressed;
            MouseLeftButtonReleased = false;
        }

        private void AssociatedObjectOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var pos = mouseEventArgs.GetPosition(AssociatedObject);
            MouseX = pos.X;
            MouseY = pos.Y;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseMove -= AssociatedObjectOnMouseMove;
        }
    }
}
