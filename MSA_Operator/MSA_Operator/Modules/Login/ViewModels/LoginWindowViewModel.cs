using MSOperatorDBService;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// @author Filip Mystek
/// </summary>
namespace Login.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        /// <summary>
        /// clear textbox button click action
        /// </summary>
        public DelegateCommand<object> ClearTextBox { get; private set; }
        /// <summary>
        /// show text in passwordbox button click action
        /// </summary>
        public DelegateCommand<object> ShowTextCommand { get; private set; }
        /// <summary>
        /// Transtion of text between passwordbox to textbox
        /// </summary>
        public DelegateCommand<object> PasswordBoxToTextBoxCommand { get; private set; }
        /// <summary>
        ///  Transtion of text between textbox to passwordbox
        /// </summary>
        public DelegateCommand<object> TextBoxToPasswordBoxCommand { get; private set; }
        /// <summary>
        /// login button click action
        /// </summary>
        public DelegateCommand<object> LoginInCommand { get; private set; }
        /// <summary>
        /// Mouse acction on textbox -leftmouse down action
        /// </summary>
        public DelegateCommand<object> LeftButtonDownTBCommand { get; private set; }
        /// <summary>
        /// Mouse action on textbox- leftmouse up action
        /// </summary>
        public DelegateCommand<object> LeftButtonUpTBCommand { get; private set; }
        /// <summary>
        /// Mouse acction on passwordbox - leftmouse down action
        /// </summary>
        public DelegateCommand<object> LeftButtonDownPBCommand { get; private set; }
        /// <summary>
        /// Mouse acction on passwordbox - leftmouse up action
        /// </summary>
        public DelegateCommand<object> LeftButtonUpPBCommand { get; private set; }
        /// <summary>
        /// Mouse acction on passwordbox - click down action
        /// </summary>
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

        private void UpdatePasswordText(object obj)
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
     
      
        private void LogIn(object obj)
        {
            PasswordBox passwordBox = obj as PasswordBox;
            if (_dbModel.ValidateOperatorCredentials(Username, passwordBox.Password))
            {             
                ErrorVisibility = Visibility.Collapsed;
                _regionManager.RequestNavigate("WindowRegion", "VehicleSelectionWindow");
            }
            else
            {
                ErrorVisibility = Visibility.Visible;
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
        /// <summary>
        /// Wrong login error visibility
        /// </summary>
        public Visibility ErrorVisibility
        {
            get => _errorVisibility;
            set
            {
                SetProperty(ref _errorVisibility, value);
            }
        }
        private System.Windows.Visibility _labelUsernameVisibility = Visibility.Hidden;

        /// <summary>
        /// Label username visibility
        /// </summary>
        public Visibility LabelUsernameVisibility
        {
            get => _labelUsernameVisibility;
            set
            {
                SetProperty(ref _labelUsernameVisibility, value);
            }
        }
    
        private string _username = "";
        /// <summary>
        /// Username text
        /// </summary>
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
            }
        }
        private string _passwordText = "";
        /// <summary>
        /// Password text
        /// </summary>
        public string PasswordText
        {
            get => _passwordText;
            set
            {
                SetProperty(ref _passwordText, value);
            }
        }

       
        private string _eyeIcon  = "../Images/Icon_visibility.png";
        /// <summary>
        /// Eye icon 
        /// </summary>
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
