using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Model.CardBuilder
{
    public class UserChangeCardBuilder
    {
        UserChangeCard card;

        public UserChangeCardBuilder(User user)
        {
            card = new UserChangeCard();
            card.Name = user.Name;
            card.Email = user.Email;
            card.Password = user.Password;
            card.Id = user.Id;
        }

        public UserChangeCardBuilder BuildNewName(string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                card.NewName = card.Name;
            }
            else
            {
                card.NewName = newName;
            }            
            return this;
        }

        public UserChangeCardBuilder BuildNewPassword(string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                card.NewPassword = card.Password;
            }
            else
            {
                card.NewPassword = newPassword;
            }            
            return this;
        }

        public UserChangeCardBuilder BuildNewEmail(string newEmail)
        {
            if (string.IsNullOrEmpty(newEmail))
            {
                card.NewEmail = card.Email;
            }
            else
            {
                card.NewEmail = newEmail;
            }           
            return this;
        }

        public UserChangeCard BuildCard()
        {            
            return card;
        }
    }
}
