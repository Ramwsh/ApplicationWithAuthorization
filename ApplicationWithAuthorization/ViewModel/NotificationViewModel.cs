using ApplicationWithAuthorization.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ApplicationWithAuthorization.ViewModel
{
    public class NotificationViewModel : INotifyPropertyChanged
    {
        private Window window;

        private string? _notificationText;

        public string NotificationText
        {
            get
            {
                return _notificationText;
            }
            set
            {
                _notificationText = value;
                OnPropertyChanged(nameof(_notificationText));
            }
        }

        public ICommand CloseWindow => new StartupWindowCommand(() =>
        {            
            window.Close();
        });

        public event PropertyChangedEventHandler PropertyChanged;

        public NotificationViewModel(Window window)
        {
            this.window = window;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
