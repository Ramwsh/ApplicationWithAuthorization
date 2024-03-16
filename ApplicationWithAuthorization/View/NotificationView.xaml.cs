using ApplicationWithAuthorization.ViewModel;
using System.Windows;

namespace ApplicationWithAuthorization.View
{    
    public partial class NotificationView : Window
    {
        public NotificationView(string notificationText)
        {
            InitializeComponent();
            DataContext = new NotificationViewModel(this) { NotificationText = notificationText };
        }
    }
}
