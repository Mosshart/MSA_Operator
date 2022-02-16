using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace MovementButton.Behaviours
{
    /// <summary>
    /// Represents mouse moving behaviour
    /// </summary>
    class MouseBehaviour : Behavior<Panel>
    {
        /// <summary>
        /// Mouse value in Y axis
        /// </summary>
        public double MouseY
        {
            get { return (double)GetValue(MouseYProperty); }
            set { SetValue(MouseYProperty, value); }
        }

        /// <summary>
        /// Mouse value in X axis
        /// </summary>
        public double MouseX
        {
            get { return (double)GetValue(MouseXProperty); }
            set { SetValue(MouseXProperty, value); }
        }
        /// <summary>
        /// Mouse left button down value 
        /// </summary>
        public bool MouseLeftButtonClicked
        {
            get { return (bool)GetValue(MouseLeftButtonClickedProperty); }
            set { SetValue(MouseLeftButtonClickedProperty, value); }
        }
        /// <summary>
        /// Mouse left button up value 
        /// </summary>
        public bool MouseLeftButtonReleased
        {
            get { return (bool)GetValue(MouseLeftButtonReleasedProperty); }
            set { SetValue(MouseLeftButtonReleasedProperty, value); }
        }

        #region dependency property 
        public static readonly DependencyProperty MouseYProperty = DependencyProperty.Register(
            "MouseY", typeof(double), typeof(MouseBehaviour), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty MouseXProperty = DependencyProperty.Register(
            "MouseX", typeof(double), typeof(MouseBehaviour), new PropertyMetadata(default(double)));

       

        public static readonly DependencyProperty MouseLeftButtonClickedProperty = DependencyProperty.Register(
            "MouseLeftButtonClicked", typeof(bool), typeof(MouseBehaviour), new PropertyMetadata(default(bool)));

       

        public static readonly DependencyProperty MouseLeftButtonReleasedProperty = DependencyProperty.Register(
            "MouseLeftButtonReleased", typeof(bool), typeof(MouseBehaviour), new PropertyMetadata(default(bool)));

        #endregion

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
