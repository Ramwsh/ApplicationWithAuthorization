using System.Windows;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.ViewModel;

namespace ApplicationWithAuthorization.View
{    
    public partial class FirstRegistrationView : Window
    {
        public FirstRegistrationView(User user)
        {
            InitializeComponent();
            DataContext = new FirstRegistrationViewModel(this, user);
        }
    }
}
