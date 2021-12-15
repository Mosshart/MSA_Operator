using System.Windows.Controls;
using System.Windows.Input;
using MovementButton.ViewModels;

namespace MovementButton.Views
{
    /// <summary>
    /// Interaction logic for MovementButton
    /// </summary>
    public partial class MovementButton : UserControl
    {
        public MovementButton()
        {
            InitializeComponent();
        }

        /*private void UIElement_OnManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            var dC = this.DataContext as MovementButtonViewModel;
            e.RoutedEvent
            //dC.Angle = GetAndle(e.Position, this.RenderSize);
        }*/
    }
}
