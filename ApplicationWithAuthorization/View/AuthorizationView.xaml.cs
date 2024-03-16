using System.Windows;
using ApplicationWithAuthorization.ViewModel;

namespace ApplicationWithAuthorization.View
{    
    public partial class AuthorizationView : Window
    {
        public AuthorizationView()
        {
            InitializeComponent();
            DataContext = new AuthorizationViewModel(this) { Login = "Имя пользователя", Password = "Пароль" };            
        }
    }
}
