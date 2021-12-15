using MSA_Operator.ViewModels.StatusBarViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MSA_Operator.ViewModels;

namespace MSA_Operator.Views
{
    /// <summary>
    /// Interaction logic for BatteryLevel.xaml
    /// </summary>
    public partial class BatteryLevel : UserControl
    {
       /*  public static readonly DependencyProperty ModelTypeDependency = DependencyProperty.Register("ModelType", typeof(int), typeof(BatteryLevelViewModel));
 
         public int ModelType
         {
             get { return (int)GetValue(ModelTypeDependency); }
             set
             {
                 //DataContex.ModelType = value;
                 SetValue(ModelTypeDependency, value);
             }
         }
         */


         public static readonly DependencyProperty ModelProperty =
             DependencyProperty.Register("Model", typeof(ModelType),
                 typeof(BatteryLevelViewModel), new PropertyMetadata(Views.ModelType.Operator));
         public ModelType Model
         {
             get { return (ModelType)GetValue(ModelProperty); }
             set
             {
                 //DataContex.ModelType = value;
                 SetValue(ModelProperty, value);
             }
         }

        // BatteryLevelViewModel DataContex;
        public BatteryLevel()
        {
            InitializeComponent();
            
           
        }
    }
    public enum ModelType
    {
        Robot = 0,
        Operator = 1
    }
}

