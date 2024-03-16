using System.Windows;
using ApplicationWithAuthorization.ViewModel;

namespace ApplicationWithAuthorization.View
{    
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel(this) { UserName = "Введите имя", Email = "Введите почту", VerificationCode = "Код подтверждения" };                            
        }
    }
}
