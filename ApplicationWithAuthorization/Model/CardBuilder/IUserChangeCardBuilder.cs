using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Model.CardBuilder
{
    interface IUserChangeCardBuilder
    {
        public UserChangeCardBuilder BuildNewName(string newName);
        public UserChangeCardBuilder BuildNewPassword(string newPassword);
        public UserChangeCardBuilder BuildNewEmail(string newEmail);
        public UserChangeCard BuildCard();
    }
}
