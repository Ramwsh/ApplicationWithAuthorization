using ApplicationWithAuthorization.Model.CRUDLogic;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.Services;
using ApplicationWithAuthorization.Services.UserChangeService;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ApplicationWithAuthorization.ViewModel
{
    public class AdminRoleViewModel : INotifyPropertyChanged
    {
        private Window currentWindow;

        private string? _selectedUserName;

        public string? SelectedUserName
        {
            get
            {
                return _selectedUserName;
            }
            set
            {
                _selectedUserName = value;
                OnPropertyChanged(nameof(_selectedUserName));
            }
        }

        private UserChangeCard _selectedUser;

        public UserChangeCard SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
                _selectedUserName = _selectedUser.Name;
                OnPropertyChanged(nameof(_selectedUserName));
                OnPropertyChanged(nameof(SelectedUserName));
                OnPropertyChanged(nameof(_selectedUser));
            }
        }

        private List<UserChangeCard> _userChangeCards;
        public List<UserChangeCard> UserChangeCards
        {
            get
            {
                return _userChangeCards;
            }
            set
            {
                _userChangeCards = value;
                OnPropertyChanged(nameof(_userChangeCards));
            }
        }

        public AdminRoleViewModel(Window window)
        {
            currentWindow = window;
            UserChangeCardCRUD crud = new UserChangeCardCRUD();
            UserChangeCards = crud.ReadUserChangeCards();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ConfirmRequest => new RegistrationCommand((obj) =>
        {
            if (_selectedUser != null && SelectedUser != null)
            {
                UserDataChangeService service = new UserDataChangeService();
                service.ChangeUserData(_selectedUser);
                service.RemoveUserRequest(_selectedUser);
                service.SendEmailNotification(_selectedUser, "Ваша заявка на изменение данных была принята", "Изменение данных безопасности");
                UserChangeCardCRUD crud = new UserChangeCardCRUD();
                UserChangeCards = crud.ReadUserChangeCards();
            }
        });

        public ICommand CancelRequest => new RegistrationCommand((obj) =>
        {
            if (_selectedUser == null && SelectedUser != null)
            {
                UserDataChangeService service = new UserDataChangeService();
                service.RemoveUserRequest(_selectedUser);
                service.SendEmailNotification(_selectedUser, "Ваша заявка на изменение данных была отклонена", "Изменение данных безопасности");
                UserChangeCardCRUD crud = new UserChangeCardCRUD();
                UserChangeCards = crud.ReadUserChangeCards();
            }
        });

        public ICommand CloseWindow => new RegistrationCommand((obj) =>
        {
            currentWindow.Close();
        });

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
