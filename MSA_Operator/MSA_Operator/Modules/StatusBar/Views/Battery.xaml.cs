using System.Windows;
using System.Windows.Controls;
using StatusBar.ViewModels;

namespace StatusBar.Views
{
    /// <summary>
    /// Interaction logic for Battery
    /// </summary>
    public partial class Battery : UserControl
    {
        #region DependencyObject
        private BatteryViewModel _viewModel
        {
            get { return this.DataContext as BatteryViewModel; }
        }

        public ModelType ModelType
        {
            get { return (ModelType)GetValue(ModelTypeProperty); }
            set { SetValue(ModelTypeProperty, value); }
        }
        public static readonly DependencyProperty ModelTypeProperty =
            DependencyProperty.Register("ModelType", typeof(ModelType), typeof(Battery),
                new PropertyMetadata(ViewModels.ModelType.Default, new PropertyChangedCallback(OnModelTypeChanged)));

        private static void OnModelTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Battery)d).OnEventIdChanged(e);
        }

        protected virtual void OnEventIdChanged(DependencyPropertyChangedEventArgs e)
        {
            this._viewModel.ModelType = (ModelType)e.NewValue;
        }

        #endregion



        public Battery()
        {
            InitializeComponent();
        }
    }
}
