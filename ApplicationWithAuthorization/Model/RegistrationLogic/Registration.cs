using ApplicationWithAuthorization.Model.CRUDLogic;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.View;

namespace ApplicationWithAuthorization.Model.RegistrationLogic
{
    public class Registration
    {        
        private bool IsValid = false;
                
        public Registration RequestValidation(User userToRegister)
        {
            RegistrationValidator validator = new RegistrationValidator();
            IsValid = validator.ValidateRegistration(userToRegister);            
            return this;
        }        

        public Registration CreatePreRegisterQuery(UserRegistration user)
        {
            if (IsValid)
            {                
                if (!string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Email))
                {
                    UserRegistrationCRUD crud = new UserRegistrationCRUD();                    
                    EmailConfirmationSender sender = new EmailConfirmationSender();                    
                    user.VerificationCode = sender.GetConfirmationCode();                    
                    crud.CreateRegistration(user);                    
                    sender.SendConfirmationCode(user);                    
                }                
            }
            else
            {
                NotificationView notifyView = new NotificationView("Пользователь с такими данными уже есть в системе");
                notifyView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                notifyView.Show();
            }
            return this;
        }
        
        public void ConfirmRegistration(UserRegistration user)
        {
            User confirmedUser = null;
            string password = new EmailConfirmationSender().GetConfirmationCode();
            if (user != null)
            {
                UserRegistrationCRUD crud = new UserRegistrationCRUD();
                var preregisteredUsers = crud.GetAllRegistrations();
                var userToConfirm = preregisteredUsers.Find(preUser => preUser.Email.Equals(user.Email) && preUser.Name.Equals(user.Name) && preUser.VerificationCode.Equals(user.VerificationCode));
                if (userToConfirm != null)
                {
                    confirmedUser = new User();
                    confirmedUser.Email = userToConfirm.Email;
                    confirmedUser.Name = userToConfirm.Name;                    
                    confirmedUser.Password = password;
                    confirmedUser.Role = "User";
                    confirmedUser.Remind = "True";
                    confirmedUser.PasswordDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString();
                    confirmedUser.PasswordHistory = string.Empty + password;
                    crud.RemoveRegistration(user);
                }
            }
            if (confirmedUser != null)
            {
                UserCRUD crud = new UserCRUD();
                crud.CreateUser(confirmedUser);
                NotificationView notifyView = new NotificationView($"Вы успешно прошли регистрацию\nВаш пароль для входа: {password}");
                notifyView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                notifyView.Show();
            }
        }
    }
}
