using System.Windows;
using ApplicationWithAuthorization.ViewModel;

namespace ApplicationWithAuthorization.View.Themes
{    
    public partial class UserSecuritySettings : Window
    {
        public UserSecuritySettings(UserSecuritySettingsViewModel userSecuritySettingsViewModel)
        {
            InitializeComponent();
            userSecuritySettingsViewModel.InitializeWindow(this);
            DataContext = userSecuritySettingsViewModel;
        }
    }
}
