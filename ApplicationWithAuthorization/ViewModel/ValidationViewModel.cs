using ApplicationWithAuthorization.Services;
using System.ComponentModel;
using System.Windows.Input;
using ApplicationWithAuthorization.Model.ValidationLogic;
using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.ViewModel
{
    public class ValidationViewModel : INotifyPropertyChanged
    {        
        public EventHandler<ValidationCompletedEventArgs> ValidationCompleted;

        private string? _passwordInput;

        private User targetUser;

        public string? PasswordInput
        {
            get
            {
                return _passwordInput;
            }
            set
            {
                _passwordInput = value;
            }
        }

        public ValidationViewModel(User user)
        {
            targetUser = user;
        }

        public event PropertyChangedEventHandler PropertyChanged;        

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));            
        }        

        public ICommand StartValidation => new RegistrationCommand((obj) =>
        {            
            bool isValidated = false;
            if (!string.IsNullOrEmpty(_passwordInput))
            {
                isValidated = new UserValidation().IsPasswordValid(_passwordInput, targetUser);                
                if (isValidated)
                {
                    ValidationCompleted?.Invoke(this, new ValidationCompletedEventArgs { IsValidated = true });
                }
                if (!isValidated)
                {
                    ValidationCompleted?.Invoke(this, new ValidationCompletedEventArgs { IsValidated = false });
                }
            }            
        });
    }    
}
