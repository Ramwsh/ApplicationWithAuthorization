using ApplicationWithAuthorization.Model.CRUDLogic;
using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Model.RegistrationLogic
{
    public class RegistrationValidator
    {                
        public bool ValidateRegistration(User userToRegister)
        {
            bool isValid = true;
            UserCRUD crud = new UserCRUD();
            List<User> users = crud.ReadUsers();

            if (users != null)
            {
                User existingUserByEmail = users.Find(user => user.Email.Equals(userToRegister.Email));
                if (existingUserByEmail != null)
                {
                    isValid = false;
                }

                User existingUserByName = users.Find(user => user.Name.Equals(userToRegister.Name));
                if (existingUserByName != null)
                {
                    isValid = false;
                }
            }

            return isValid;
        }                
    }
}
