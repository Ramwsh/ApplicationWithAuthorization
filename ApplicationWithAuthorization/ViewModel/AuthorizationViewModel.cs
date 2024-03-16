using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ApplicationWithAuthorization.Model.AuthorizationLogic;
using ApplicationWithAuthorization.Services;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.View;
using ApplicationWithAuthorization.Model.PasswordRules;

namespace ApplicationWithAuthorization.ViewModel
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        private int enterAttempts = 0;
        private string? _login;
        private string? _password;
        private Window window;

        private User _authUser;

        public string? Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged(_login);
            }
        }

        public string? Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(_password);
            }
        }

        public ICommand ShutDownProgrammCommand => new RegistrationCommand((obj) =>
        {
            Environment.Exit(0);
        });

        public ICommand AuthorizeUser => new RegistrationCommand((obj) => 
        {            
            if (!string.IsNullOrEmpty(_login) && !string.IsNullOrEmpty(_password))
            {
                _authUser = new User() { Name = _login, Password = _password };
                AuthorizationValidator validator = new AuthorizationValidator();
                var isExists = validator.ValidateUserExists(_authUser);
                if (isExists)
                {
                    var authorizedUser = validator.ValidateAuthorization(_authUser);
                    if (authorizedUser != null)
                    {
                        _authUser = authorizedUser;
                        if (_authUser.Name != "admin")
                        {
                            PasswordRulesValidation validation = new PasswordRulesValidation();
                            PasswordRules validationRules = new PasswordRules();
                            validation.SetCondition(new PasswordConditions { conditionName = "Срок пароля истек.\n Настойчиво рекомендуем его сменить" })
                            .SetRules(() => validationRules.CheckOutdatedRule(_authUser));
                            var validationResult = validation.ProcessValidations();
                            if (validationResult != null && validationResult.Count > 0 && validationResult[0].conditionResult == true)
                            {
                                string notificationText = validationResult[0].conditionName;
                                NotificationView notifyView = new NotificationView(notificationText);
                                notifyView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                notifyView.ShowDialog();
                            }
                        }                        
                        HomeView homeWindow = new HomeView(_authUser);                        
                        homeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;                        
                        homeWindow.Show();                        
                        window.Close();                        
                    }
                    else
                    {
                        enterAttempts++;
                        if (enterAttempts == 3)
                        {
                            NotificationView notifyView = new NotificationView("Ошибка входа\nИспользовано попыток - 3\nОжидание. Данное окно закроется автоматически.");
                            notifyView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            notifyView.Show();
                            Thread.Sleep(10000);
                            notifyView.Close();
                        }                        
                        else
                        {
                            NotificationView notifyView = new NotificationView("Ошибка входа");
                            notifyView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            notifyView.Show();
                        }
                        
                    }
                }
                else
                {
                    NotificationView notifyView = new NotificationView("Такого пользователя не существует");
                    notifyView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    notifyView.Show();
                }
            }            
        });

        public ICommand ReturnToStartupView => new StartupWindowCommand(() => {
            MainWindow startupWindow = new MainWindow();
            startupWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            startupWindow.Show();
            window.Close();
        });

        public AuthorizationViewModel(Window window)
        {
            this.window = window;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
