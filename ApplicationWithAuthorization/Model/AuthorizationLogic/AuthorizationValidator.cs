using ApplicationWithAuthorization.Model.CRUDLogic;
using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Model.AuthorizationLogic
{
    public class AuthorizationValidator
    {
        public bool ValidateUserExists(User user)
        {
            if (user != null)
            {
                UserCRUD crud = new UserCRUD();
                var users = crud.ReadUsers();
                if (users != null)
                {
                    if (users.Any(dbusers => dbusers.Name.Equals(user.Name)))
                    {                        
                        return true;
                    }
                    else
                    {                        
                        return false;
                    }
                }
            }            
            return false;
        }

        public User ValidateAuthorization(User user)
        {
            if (user != null)
            {
                UserCRUD crud = new UserCRUD();
                var users = crud.ReadUsers();
                if (users != null)
                {
                    User userToMatch = users.FirstOrDefault(usersdb => usersdb.Name.Equals(user.Name) && usersdb.Password.Equals(user.Password));
                    if (userToMatch != null)
                    {                        
                        return userToMatch;
                    }
                }
            }            
            return null;
        }
    }
}
