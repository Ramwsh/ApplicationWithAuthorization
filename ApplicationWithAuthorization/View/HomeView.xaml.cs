using System.Windows;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.ViewModel;

namespace ApplicationWithAuthorization.View
{    
    public partial class HomeView : Window
    {        

        public HomeView(User user)
        {
            InitializeComponent();
            User currentUser = user;
            DataContext = new HomeViewModel(user, this);
        }
    }
}
