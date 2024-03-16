using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.Services;
using ApplicationWithAuthorization.View;

namespace ApplicationWithAuthorization.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private User currentUser;
        private Window homeWindow;

        private string? _userRole;

        public string? UserRole
        {
            get
            {
                return _userRole;
            }
            set
            {
                _userRole = currentUser.Role;
                OnPropertyChanged(nameof(_userRole));
            }
        }

        public ICommand OpenAdminMode => new StartupWindowCommand(() =>
        {
            if (currentUser.Role == "Admin")
            {
                AdminRoleView view = new AdminRoleView();
                view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                view.ShowDialog();
            }            
            else
            {
                NotificationView notifyView = new NotificationView("Ваша роль не администратор");
                notifyView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                notifyView.ShowDialog();
            }
        });

        public ICommand OpenUserSettingsWindow => new StartupWindowCommand(() =>
        {
            UserSettingsView userSettingsWindow = new UserSettingsView(currentUser);
            userSettingsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            userSettingsWindow.ShowDialog();
            homeWindow.Close();
        });

        public ICommand ShutDownProgrammCommand => new RegistrationCommand((obj) =>
        {
            Environment.Exit(0);
        });

        public ICommand OpenUserListWindow
        {
            get
            {
                return new StartupWindowCommand(() =>
                {
                    UserRoleView view = new UserRoleView(currentUser);
                    view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    view.Show();
                });
            }
        }

        public HomeViewModel(User user, Window window)
        {
            currentUser = user;
            homeWindow = window;
            UserRole = user.Role;
            if (currentUser.Remind == "True")
            {
                Console.WriteLine(currentUser.Remind);
                FirstRegistrationView firstRegistrationView = new FirstRegistrationView(currentUser);
                firstRegistrationView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                firstRegistrationView.ShowDialog();
            }
        }        

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
