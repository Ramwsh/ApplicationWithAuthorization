using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Model.ValidationLogic
{
    public class UserValidation
    {
        public bool IsPasswordValid(string passwordInput, User targetUser)
        {
            string passwordToMatch = targetUser.Password;
            if (passwordToMatch.Equals(passwordInput))
            {
                return true;
            }
            return false;
        }
    }
}
