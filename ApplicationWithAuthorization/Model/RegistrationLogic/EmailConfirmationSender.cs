using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Model.RegistrationLogic
{
    class EmailConfirmationSender
    {
        private string? confirmationCode;        

        public EmailConfirmationSender()
        {            
            GenerateConfirmationCode();
        }        

        public void SendConfirmationCode(UserRegistration userToRegister)
        {
            if (userToRegister != null)
            {
                if (!string.IsNullOrEmpty(userToRegister.Email))
                {
                    EmailSender sender = new EmailSender();                                        
                    string subject = "Код подтверждения регистрации пользователя в системе";
                    sender.SendMessage(userToRegister.Email, confirmationCode, subject);
                }
            }
        }

        private void GenerateConfirmationCode()
        {
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int codeLength = 10;
            Random random = new Random();
            confirmationCode = new string(Enumerable.Repeat(allowedChars, codeLength).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GetConfirmationCode()
        {
            return confirmationCode;
        }
    }
}
