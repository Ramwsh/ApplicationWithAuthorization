using System.Windows;
using ApplicationWithAuthorization.ViewModel;

namespace ApplicationWithAuthorization.View
{    
    public partial class AdminRoleView : Window
    {
        public AdminRoleView()
        {
            InitializeComponent();
            DataContext = new AdminRoleViewModel(this);
        }
    }
}
