using ApplicationWithAuthorization.Model.CardBuilder;
using ApplicationWithAuthorization.Model.CRUDLogic;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.Model.PasswordRules;
using ApplicationWithAuthorization.Services;
using ApplicationWithAuthorization.View;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ApplicationWithAuthorization.ViewModel
{
    public class UserSecuritySettingsViewModel : INotifyPropertyChanged
    {
        private Window currentWindow;

        private string? _newUserName;
        public string? NewUserName
        {
            get
            {
                return _newUserName;
            }
            set
            {
                _newUserName = value;
                OnPropertyChanged(nameof(_newUserName));
            }
        }

        private string? _newUserPassword;
        public string? NewUserPassword
        {
            get
            {
                return _newUserPassword;
            }
            set
            {
                _newUserPassword = value;
                OnPropertyChanged(nameof(_newUserPassword));
            }
        }

        private string? _newUserEmail;
        public string? NewUserEmail
        {
            get
            {
                return _newUserEmail;
            }
            set
            {
                _newUserEmail = value;
                OnPropertyChanged(nameof(_newUserEmail));
            }
        }

        private string? _currentUserName;        
        public string? CurrentUserName
        {
            get
            {
                return _currentUserName;
            }
            set
            {
                _currentUserName = value;
                OnPropertyChanged(nameof(_currentUserName));
            }
        }

        private string? _currentUserEmail;
        public string? CurrentUserEmail
        {
            get
            {
                return _currentUserEmail;
            }
            set
            {
                _currentUserEmail = value;
                OnPropertyChanged(nameof(_currentUserEmail));
            }
        }

        private string? _currentUserPassword;
        public string? CurrentUserPassword
        {
            get
            {
                return _currentUserPassword;
            }
            set
            {
                _currentUserPassword = value;
                OnPropertyChanged(nameof(_currentUserPassword));
            }
        }


        private User currentUser;

        public event PropertyChangedEventHandler PropertyChanged;

        public UserSecuritySettingsViewModel(User user)
        {            
            currentUser = user;
            CurrentUserEmail = currentUser.Email;
            CurrentUserPassword = currentUser.Password;
            Console.WriteLine(user.Password);
            CurrentUserName = currentUser.Name;
        }

        public void InitializeWindow(Window window)
        {
            currentWindow = window;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand CloseWindow => new StartupWindowCommand(() =>
        {
            currentWindow.Close();
        });

        public ICommand CreateUserChangeCardRequest => new RegistrationCommand((obj) => 
        {
            PasswordRulesValidation validation = new PasswordRulesValidation();
            PasswordRules validationRules = new PasswordRules();
            validation
            .SetCondition(new PasswordConditions { conditionName = "Длина пароля не менее 10 символов" })
            .SetRules(() => validationRules.LengthRule(_newUserPassword))
            .SetCondition(new PasswordConditions { conditionName = "Должны использоваться различные группы символов" })
            .SetRules(() => validationRules.MultiRegRule(_newUserPassword))
            .SetCondition(new PasswordConditions { conditionName = "Такой пароль был ранее использован" })
            .SetRules(() => validationRules.CheckPasswordHistory(_newUserPassword, currentUser));

            var validationResult = validation.ProcessValidations();            

            if (validationResult.All(condition => condition.conditionResult))
            {
                UserChangeCardBuilder cardBuilder = new UserChangeCardBuilder(currentUser);
                cardBuilder.BuildNewEmail(_newUserEmail).BuildNewName(_newUserName).BuildNewPassword(_newUserPassword);
                UserChangeCard card = cardBuilder.BuildCard();

                UserChangeCardCRUD crud = new UserChangeCardCRUD();
                crud.CreateUserChangeCard(card);
                NotificationView notifyView = new NotificationView("Запрос на изменения отправлен администратору");
                notifyView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                notifyView.Show();
            }
            else
            {
                string errors = string.Empty;
                foreach (var results in validationResult)
                {
                    if (!results.conditionResult)
                    {
                        errors += results.conditionName + "\n";
                    }                    
                }
                NotificationView notifyView = new NotificationView(errors);
                notifyView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                notifyView.Show();
            }        
        });
    }
}
