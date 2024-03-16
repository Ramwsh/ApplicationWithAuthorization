using ApplicationWithAuthorization.Services;
using System.ComponentModel;
using System.Windows.Input;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.Model.RegistrationLogic;
using System.Windows;
using ApplicationWithAuthorization.View;

namespace ApplicationWithAuthorization.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private string? _userName;
        private string? _email;
        private string? _verificationCode;

        private UserRegistration _userRegistration;
        private Window window;

        public RegistrationViewModel(Window window)
        {
            this.window = window;
        }

        public ICommand BeginRegistration => new StartupWindowCommand(() =>
        {
            MainWindow startupView = new MainWindow();
            startupView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            startupView.Show();

            window.Close();
        });

        public ICommand ShutDownProgrammCommand => new RegistrationCommand((obj) =>
        {
            Environment.Exit(0);
        });

        public ICommand SendRegistationRequest
        {
            get
            {
                return new RegistrationCommand((obj) =>
                {
                    if (!string.IsNullOrWhiteSpace(_userName) && !string.IsNullOrWhiteSpace(Email))
                    {
                        _userRegistration = new UserRegistration() { Email = _email, Name = _userName };
                        Registration registration = new Registration();
                        registration.RequestValidation(new User() { Email = _email, Name = _userName }).CreatePreRegisterQuery(_userRegistration);
                    }
                    else
                    {
                        NotificationView notifyView = new NotificationView("Ошибка в вводе почты и имени");
                        notifyView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        notifyView.Show();                        
                    }
                });
            }
        }

        public ICommand ConfirmRegistrationRequest
        {
            get
            {
                return new RegistrationCommand((obj) => {

                    if (_userRegistration.Name.Equals(_userName) && _userRegistration.Email.Equals(_email))
                    {
                        _userRegistration = new UserRegistration() { Email = _email, Name = _userName, VerificationCode = _verificationCode };
                        Registration registration = new Registration();
                        registration.ConfirmRegistration(_userRegistration);
                    }
                    else
                    {
                        NotificationView notifyView = new NotificationView("Не удается подтвердить регистрацию");
                        notifyView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        notifyView.Show();
                    }
                });
            }
        }

        public string? UserName 
        { 
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(_userName));
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(_email));
            }
        }

        public string? VerificationCode
        {
            get => _verificationCode;
            set
            {
                _verificationCode = value;
                OnPropertyChanged(nameof(_verificationCode));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
