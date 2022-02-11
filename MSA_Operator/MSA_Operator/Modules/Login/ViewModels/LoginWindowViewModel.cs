using MSOperatorDBService;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;

namespace Login.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        public DelegateCommand<object> ClearTextBox { get; private set; }
        public DelegateCommand<object> ShowTextCommand { get; private set; }
        public DelegateCommand<object> PasswordBoxToTextBoxCommand { get; private set; }
        public DelegateCommand<object> TextBoxToPasswordBoxCommand { get; private set; }
        public DelegateCommand<object> LoginInCommand { get; private set; }
        public DelegateCommand<object> LeftButtonDownTBCommand { get; private set; }
        public DelegateCommand<object> LeftButtonUpTBCommand { get; private set; }
        public DelegateCommand<object> LeftButtonDownPBCommand { get; private set; }
        public DelegateCommand<object> LeftButtonUpPBCommand { get; private set; }
        public DelegateCommand<object> UpdatePasswordTextOnClick { get; private set; }

        private readonly IRegionManager _regionManager;
        private DatabaseModel _dbModel;
        public LoginWindowViewModel(IRegionManager regionManager, DatabaseModel dbModel)
        {
            _regionManager = regionManager;
            _dbModel = dbModel;

            ClearTextBox = new DelegateCommand<object>(ClearText);
            //  ShowTextCommand = new DelegateCommand<object>(ShowPasswordText);
            LoginInCommand = new DelegateCommand<object>(LogIn);

            PasswordBoxToTextBoxCommand = new DelegateCommand<object>(PasswordBoxToTextBox);
            TextBoxToPasswordBoxCommand = new DelegateCommand<object>(TextBoxToPasswordBox);

            LeftButtonDownTBCommand = new DelegateCommand<object>(LeftButtonDownTB);
            LeftButtonUpTBCommand = new DelegateCommand<object>(LeftButtonUpTB);
            LeftButtonDownPBCommand = new DelegateCommand<object>(LeftButtonDownPB);
            LeftButtonUpPBCommand = new DelegateCommand<object>(LeftButtonUpPB);
            UpdatePasswordTextOnClick = new DelegateCommand<object>(UpdatePasswordText);
        }

        void UpdatePasswordText(object obj)
        {
                TextBox a = obj as TextBox;

                if (a.Text != PasswordText)
                    PasswordText = a.Text;            
        }

        private void LeftButtonUpTB(object passwordBox)
        {
            TextBox a = passwordBox as TextBox;

            a.Visibility = Visibility.Hidden;
        }

        private void LeftButtonDownTB(object textBox)
        {
            TextBox a = textBox as TextBox;
           
            a.Visibility = Visibility.Visible;
            a.CaretIndex = a.Text.Length;


            a.Focus();
        }
        private void LeftButtonUpPB(object passwordBox)
        {
            PasswordBox a = passwordBox as PasswordBox;

            EyeIcon = "../Images/Icon_visibility.png";
            a.Visibility = Visibility.Visible;
            a.Password = PasswordText;

            a.Focus();
        }

        private void LeftButtonDownPB(object passwordBox)
        {
            PasswordBox a = passwordBox as PasswordBox;

            a.Visibility = Visibility.Hidden;
            EyeIcon = "../Images/Icon_visibility_off.png";
        }
        private void PasswordBoxToTextBox(object obj)
        {
            PasswordBox passwordBox = obj as PasswordBox;
            if (passwordBox.Password != PasswordText)
                PasswordText = passwordBox.Password;
        }

        private void TextBoxToPasswordBox(object obj)
        {
            PasswordBox passwordBox = obj as PasswordBox;
            if(passwordBox.Password != PasswordText)
                passwordBox.Password = PasswordText;
        }
        /*private void ShowPasswordText(object passwordBox)
        {
            TextBox a = passwordBox as TextBox;
            if (a.Visibility == Visibility.Hidden )
                a.Visibility = Visibility.Visible;
            else
                a.Visibility = Visibility.Hidden;
        }*/

      


        private void LogIn(object obj)
        {
            // if (Username.ToLower() == "admin" && passwordBox.Password == "admin" || Username.ToLower() == "" && passwordBox.Password == "")
            PasswordBox passwordBox = obj as PasswordBox;
            if (_dbModel.ValidateOperatorCredentials(Username, passwordBox.Password))
            {
                //_regionManager.RequestNavigate("MainRegion", "Map");
                // _regionManager.RequestNavigate("WindowRegion","VehicleSelectionWindow" );
                ErrorVisibility = Visibility.Collapsed;
                _regionManager.RequestNavigate("WindowRegion", "VehicleSelectionWindow");
            }
            else
            {

                ErrorVisibility = Visibility.Visible;
                // _regionManager.RequestNavigate("WindowRegion", "VehicleSelectionWindow");
            }
            Username = "";
            passwordBox.Password = "";
        }
        private void ClearText(object Visibility)
        {
            Visibility vis =(Visibility) Visibility;
            if (vis == System.Windows.Visibility.Visible)
            {
                Username = "";
            }
        }
        
        private Visibility _errorVisibility = Visibility.Collapsed;

        public Visibility ErrorVisibility
        {
            get => _errorVisibility;
            set
            {
                SetProperty(ref _errorVisibility, value);
            }
        }
        private System.Windows.Visibility _labelUsernameVisibility = Visibility.Hidden;

        public Visibility LabelUsernameVisibility
        {
            get => _labelUsernameVisibility;
            set
            {
                SetProperty(ref _labelUsernameVisibility, value);
            }
        }
        public Visibility SetVisible()
        {
            LabelUsernameVisibility = Visibility.Visible;
            return  Visibility.Visible;
        }
        private string _username = "";

        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
            }
        }
        private string _passwordText = "";
        private string _passwordTrueText = "";
        private bool _isPasswordShown = false;
        public string PasswordText
        {
            get => _passwordText;
            set
            {
                SetProperty(ref _passwordText, value);
            }
        }

        private Visibility _paswordVisiblePB = Visibility.Visible;
        private Visibility PaswordVisiblePB
        {
            get => _paswordVisiblePB;
            set
            {
                SetProperty(ref _paswordVisiblePB, value);
            }
        }
        private Visibility _paswordVisibleTB = Visibility.Visible;
        private Visibility PaswordVisibleTB
        {
            get => _paswordVisibleTB;
            set
            {
                SetProperty(ref _paswordVisibleTB, value);
            }
        }

        private string _eyeIcon  = "../Images/Icon_visibility.png";
        public string EyeIcon
        {
            get => _eyeIcon;
            set
            {
                SetProperty(ref _eyeIcon, value);
            }
        }
    }
}
