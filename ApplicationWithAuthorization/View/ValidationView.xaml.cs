using System.Windows;
using ApplicationWithAuthorization.ViewModel;

namespace ApplicationWithAuthorization.View
{    
    public partial class ValidationView : Window
    {                
        public ValidationView(ValidationViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }      
        
        public void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
