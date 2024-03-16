using System.Windows;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.ViewModel;

namespace ApplicationWithAuthorization.View
{    
    public partial class UserSettingsView : Window
    {
        public UserSettingsView(User user)
        {            
            InitializeComponent();
            User currentUser = user;
            DataContext = new UserSettingsViewModel(currentUser, this)
            {
                UserLogin = user.Name,
                UserEmail = "??????",
                UserId = "??????",
                UserPassword = "??????",
                UserRole = user.Role
            };
        }
    }
}
