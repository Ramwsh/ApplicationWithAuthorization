using ApplicationWithAuthorization.Model.Database.Contexts;
using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Model.CRUDLogic
{
    public class UserCRUD : ICRUD
    {
        public void CreateUser(User user)
        {
            if (user != null)
            {
                using (UserDbContext context = new UserDbContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }            
        }

        public void DeleteUser(User user)
        {
            if (user != null)
            {
                using (UserDbContext context = new UserDbContext())
                {
                    User userToDelete = context.Users.Find(user.Id);
                    context.Remove(userToDelete);
                    context.SaveChanges();
                }
            }            
        }

        public List<User> ReadUsers()
        {
            using (UserDbContext context = new UserDbContext())
            {
                return context.Users.ToList();
            }
        }

        public void UpdateUser(User user, User updatedUser)
        {
            if (user != null && updatedUser != null)
            {
                using (UserDbContext context = new UserDbContext())
                {
                    User userToUpdate = context.Users.Find(user.Id);
                    userToUpdate.Name = updatedUser.Name;
                    userToUpdate.Password = updatedUser.Password;
                    userToUpdate.Email = updatedUser.Email;
                    userToUpdate.Role = updatedUser.Role;
                    userToUpdate.PasswordDate = updatedUser.PasswordDate;
                    userToUpdate.Remind = "False";
                    userToUpdate.PasswordHistory = updatedUser.PasswordHistory;
                    context.SaveChanges();
                }
            }            
        }

        public void UpdateUser(User user)
        {
            if (user != null)
            {
                using (UserDbContext context = new UserDbContext())
                {                                                            
                    var targetUser = context.Users.Where(u => u.Name.Equals(user.Name)).FirstOrDefault();
                    if (targetUser != null)
                    {
                        targetUser.Name = user.Name;
                        targetUser.Role = user.Role;
                        targetUser.Email = user.Email;
                        targetUser.Remind = user.Remind;
                        targetUser.PasswordDate = user.PasswordDate;
                        targetUser.PasswordHistory = user.PasswordHistory;
                        targetUser.Password = user.Password;
                        context.SaveChanges();                        
                    }                    
                }
            }
        }
    }
}
