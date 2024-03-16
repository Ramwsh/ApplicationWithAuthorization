using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ApplicationWithAuthorization.Model.CRUDLogic;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.Model.PasswordRules;
using ApplicationWithAuthorization.Services;
using ApplicationWithAuthorization.View;

namespace ApplicationWithAuthorization.ViewModel
{
    public class FirstRegistrationViewModel : INotifyPropertyChanged
    {
        private User currentUser;

        private Window _currentWindow;

        private string? _passwordInput;

        public string? PasswordInput
        {
            get
            {
                return _passwordInput;
            }
            set
            {
                _passwordInput = value;
            }
        }

        public FirstRegistrationViewModel(Window window, User currentUser)
        {
            _currentWindow = window;
            this.currentUser = currentUser;
        }

        public ICommand CloseFirstRegistrationWindow => new RegistrationCommand((obj) =>
        {
            Environment.Exit(0);
        });

        public ICommand PasswordChangeCommand => new RegistrationCommand((obj) =>
        {
            if (!string.IsNullOrEmpty(_passwordInput) && !string.IsNullOrEmpty(PasswordInput))
            {

                PasswordRulesValidation validation = new PasswordRulesValidation();
                PasswordRules validationRules = new PasswordRules();
                validation
                .SetCondition(new PasswordConditions { conditionName = "Длина пароля не менее 10 символов" })
                .SetRules(() => validationRules.LengthRule(_passwordInput))
                .SetCondition(new PasswordConditions { conditionName = "Должны использоваться различные группы символов" })
                .SetRules(() => validationRules.MultiRegRule(_passwordInput));

                var validationResult = validation.ProcessValidations();

                if (validationResult.All(condition => condition.conditionResult))
                {
                    User newUserModel = new User();
                    newUserModel.Name = currentUser.Name;
                    newUserModel.Email = currentUser.Email;
                    newUserModel.Password = _passwordInput;
                    newUserModel.PasswordDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString();
                    newUserModel.PasswordHistory = currentUser.PasswordHistory+$"\n{_passwordInput}";
                    newUserModel.Id = currentUser.Id;
                    newUserModel.Role = currentUser.Role;
                    newUserModel.Remind = "False";

                    UserCRUD crud = new UserCRUD();
                    crud.UpdateUser(currentUser, newUserModel);
                    NotificationView notifyView = new NotificationView("Ваш пароль был изменен\nОкно автоматически закроется через 10 секунд\nПерезайдите.");
                    notifyView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    notifyView.Show();
                    Thread.Sleep(10000);
                    Environment.Exit(0);
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

            }
        });

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
