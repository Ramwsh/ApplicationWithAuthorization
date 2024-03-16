using ApplicationWithAuthorization.Services;
using ApplicationWithAuthorization.View;
using System.Windows;
using System.Windows.Input;

namespace ApplicationWithAuthorization.ViewModel
{
    public class StartupWindowViewModel
    {
        private Window _window;

        public StartupWindowViewModel(Window window)
        {
            _window = window;
        }

        public ICommand ShutDownProgrammCommand => new RegistrationCommand((obj) =>
        {
            Environment.Exit(0);
        });

        public ICommand BeginAuthorization => new StartupWindowCommand(() =>
        {
            AuthorizationView authView = new AuthorizationView();
            authView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            authView.Show();
            _window.Close();
        });

        public ICommand BeginRegistration => new StartupWindowCommand(() =>
        {
            RegisterView registerView = new RegisterView();
            registerView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            registerView.Show();
            _window.Close();
        });
    }
}
