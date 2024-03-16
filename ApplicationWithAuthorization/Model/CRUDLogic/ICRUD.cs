using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Model.CRUDLogic
{
    interface ICRUD
    {
        void CreateUser(User user);
        List<User> ReadUsers();
        void UpdateUser(User user, User updatedUser);
        void DeleteUser(User user);        
    }
}
