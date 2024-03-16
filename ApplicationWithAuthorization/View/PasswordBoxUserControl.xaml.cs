using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ApplicationWithAuthorization.View
{    
    public partial class PasswordBoxUserControl : UserControl
    {


        public string PasswordProperty
        {
            get { return (string)GetValue(PasswordPasswordProperty); }
            set { SetValue(PasswordPasswordProperty, value); }
        }
        
        public static readonly DependencyProperty PasswordPasswordProperty =
            DependencyProperty.Register("PasswordProperty", typeof(string), typeof(PasswordBoxUserControl), new PropertyMetadata(string.Empty));


        public PasswordBoxUserControl()
        {
            InitializeComponent();            
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordProperty = PasswordBox.Password;
        }

        private void PasswordBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
