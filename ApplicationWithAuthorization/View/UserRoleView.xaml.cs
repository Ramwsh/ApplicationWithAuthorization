using System.Windows;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.ViewModel;

namespace ApplicationWithAuthorization.View
{    
    public partial class UserRoleView : Window
    {
        public UserRoleView(User user)
        {
            InitializeComponent();
            DataContext = new UserRoleViewModel(this, user);
        }
    }
}
