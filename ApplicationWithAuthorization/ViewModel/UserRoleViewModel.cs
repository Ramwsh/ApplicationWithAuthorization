using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ApplicationWithAuthorization.Model.CRUDLogic;

namespace ApplicationWithAuthorization.ViewModel
{
    class UserRoleViewModel : INotifyPropertyChanged
    {
        private User _currentUser;

        private User _selectedUser;

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

        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
                SelectedUserName = _selectedUser.Name;
                OnPropertyChanged(nameof(_selectedUserName));
                OnPropertyChanged(nameof(SelectedUserName));
                OnPropertyChanged(nameof(_selectedUser));
            }
        }

        private List<User> _usersList;

        public List<User> UsersList
        {
            get
            {
                return _usersList;
            }
            set
            {
                _usersList = value;
                OnPropertyChanged(nameof(_usersList));
            }
        }

        private Window currentWindow;

        public event PropertyChangedEventHandler PropertyChanged;

        private void SetUser(User user)
        {
            _currentUser = user;
        }

        public UserRoleViewModel(Window window, User user)
        {            
            _currentUser= user;
            currentWindow = window;
            UserCRUD crud = new UserCRUD();
            UsersList = crud.ReadUsers();
        }

        public ICommand SendMessageCommand => new RegistrationCommand((obj) =>
        {
            if (SelectedUser != null && _selectedUser != null)
            {
                Console.WriteLine(_currentUser.Name);
            }
        });

        public ICommand CloseWindowCommand => new RegistrationCommand((obj) =>
        {
            currentWindow.Close();
        });

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
