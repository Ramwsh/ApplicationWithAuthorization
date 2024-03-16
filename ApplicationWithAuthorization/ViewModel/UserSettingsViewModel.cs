using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.Services;
using ApplicationWithAuthorization.View;
using ApplicationWithAuthorization.View.Themes;

namespace ApplicationWithAuthorization.ViewModel
{
    // Клас представления модели для окна MainWindow.xaml.
    public class UserSettingsViewModel : INotifyPropertyChanged
    {        
        // Ссылка на текущего пользователя.
        // Ссылка на текущего пользвователя нужна для инициализации объекта пользователя по ней
        // Свойства объекта пользвователя будут проверятся при валидации в системе.
        private User currentUser;
        // Ссылка на окно. Инициализируется в конструкторе.
        // Данная ссылка необходима для выполнения команды ICommand CloseWindowCommand при закрытии окна, при успешной валидации.
        private Window window;        

        // Id текущего пользователя. Необходимо для поиска данного юзера в БД по ID. Используется в комманде ICommand ValidateUser при валидации.
        private string? _userId;

        // Ссылка на окно валидации при авторизации.
        private ValidationView validationView;

        // Команда для закрытия окна валидации и высвобождения ресурсов. Так же удаляется объект текущего класса (UserSettingsViewModel) сборщиком мусора.
        // Помимо этого комманда возвращает и фокусирует на домашнюю страницу
        public ICommand CloseWindowCommand => new RegistrationCommand((obj) =>
        {
            HomeView view = new HomeView(currentUser);
            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            view.Show();
            window.Close();
        });

        // Комманда, вызываемая при валидации пользователя в системе. Если пользователь валидирован - переходим в окно настроек безопасности пользователя (т.е даем разрешение )
        // Иначе указываем на ошибку, при ошибке валидации пользователя в системе.
        public ICommand GiveTweakingPermissions => new RegistrationCommand((obj) =>
        {
            ValidationViewModel validationViewModel = new ValidationViewModel(currentUser);
            validationViewModel.ValidationCompleted += (sender, e) =>
            {
                if (e.IsValidated)
                {                    
                    validationView.Close();
                    UserSecuritySettingsViewModel userSecuritySettingsViewModel = new UserSecuritySettingsViewModel(currentUser);
                    UserSecuritySettings userSecuritySettingsWindow = new UserSecuritySettings(userSecuritySettingsViewModel);                    
                    userSecuritySettingsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    userSecuritySettingsWindow.ShowDialog();
                }
                else
                {
                    NotificationView notifiyView = new NotificationView("Ошибка подтверждения");
                    notifiyView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    notifiyView.Show();
                }
            };
            validationView = new ValidationView(validationViewModel);
            validationView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            validationView.ShowDialog();
        });

        // Комманда, вызываемая при валидации пользователя в системе. Если пользователь валидирован - переходим в окно настроек безопасности пользователя (т.е даем разрешение )
        // Иначе указываем на ошибку, при ошибке валидации пользователя в системе.
        public ICommand ValidateCurrentUser => new RegistrationCommand((obj) => 
        {                        
            ValidationViewModel validationViewModel = new ValidationViewModel(currentUser);
            validationViewModel.ValidationCompleted += (sender, e) =>
            {
                if (e.IsValidated)
                {                    
                    UserId = Convert.ToString(currentUser.Id);
                    UserEmail = currentUser.Email;
                    UserPassword = currentUser.Password;        
                    OnPropertyChanged(nameof(UserId));
                    OnPropertyChanged(nameof(UserEmail));
                    OnPropertyChanged(nameof(UserPassword));
                    validationView.Close();
                }
                else
                {                    
                    NotificationView notifiyView = new NotificationView("Ошибка подтверждения");
                    notifiyView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    notifiyView.Show();
                }                
            };
            validationView = new ValidationView(validationViewModel);
            validationView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            validationView.ShowDialog();            
        });

        // Свойства-поля необходимые для привязки представления модели ViewModel к внешней части View.
        public string? UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(_userId));
            }
        }

        private string? _userLogin;

        public string? UserLogin
        {
            get
            {
                return _userLogin;
            }
            set
            {
                _userLogin = value;
                OnPropertyChanged(nameof(_userLogin));
            }
        }

        private string? _userPassword;

        public string? UserPassword
        {
            get
            {
                return _userPassword;
            }
            set
            {
                _userPassword = value;
                OnPropertyChanged(nameof(_userPassword));
            }
        }

        private string? _userEmail;

        public string? UserEmail
        {
            get
            {
                return _userEmail;
            }
            set
            {
                _userEmail = value;
                OnPropertyChanged(nameof(_userEmail));
            }
        }

        private string? _userRole;
        public string? UserRole
        {
            get
            {
                return _userRole;
            }
            set
            {
                _userRole = value;
                OnPropertyChanged(nameof(_userRole));
            }
        }

        // событие PropertyChangedEventHandler для реализации интерфейса INotifyPropertyChanged и реализация обработчика события OnPropertyChanged.
        // необходимо для обновления данных во внешней части программы при использовании паттерна MVVM.
        public event PropertyChangedEventHandler PropertyChanged;

        // Конструктор класса
        public UserSettingsViewModel(User user, Window window)
        {
            currentUser = user;
            this.window = window;
        }

        // Обработчик события OnPropertyChanged.
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
