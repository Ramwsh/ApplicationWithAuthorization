using ApplicationWithAuthorization.Model.CRUDLogic;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.Model.RegistrationLogic;

namespace ApplicationWithAuthorization.Services.UserChangeService
{
    public class UserDataChangeService
    {
        public void ChangeUserData(UserChangeCard card)
        {
            UserCRUD crud = new UserCRUD();
            var targetUser = crud.ReadUsers().Where(user => user.Id == card.Id).FirstOrDefault();            
            if (targetUser != null)
            {
                string passwordHistory = targetUser.Password;
                string passwordHistoryToAdd = card.NewPassword;
                User updatedUserModel = new User()
                {
                    Id = targetUser.Id,
                    Name = card.NewName,
                    Email = card.NewEmail,
                    Password = card.NewPassword,
                    Role = "User",
                    Remind = "False",
                    PasswordDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString(),
                    PasswordHistory = passwordHistory + $"\n{passwordHistoryToAdd}"
                };
                crud.UpdateUser(targetUser, updatedUserModel);
            }
        }

        public void RemoveUserRequest(UserChangeCard card)
        {
            UserChangeCardCRUD crud = new UserChangeCardCRUD();
            crud.DeleteUserChangeCard(card);
        }

        public void SendEmailNotification(UserChangeCard card, string message, string subject)
        {
            EmailSender sender = new EmailSender();
            sender.SendMessage(card.NewEmail, message, subject);
        }
    }
}
