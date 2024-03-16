using ApplicationWithAuthorization.Model.Database.Contexts;
using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.View;

namespace ApplicationWithAuthorization.Model.CRUDLogic
{
    public class UserRegistrationCRUD : IRegCRUD
    {
        public List<UserRegistration> GetAllRegistrations()
        {
            using (UserRegistrationDbContext context = new UserRegistrationDbContext())
            {
                List<UserRegistration> userRegistrations = context.UserRegistrations.ToList();
                return userRegistrations;
            }
        }

        public void RemoveRegistration(UserRegistration user)
        {
            if (user != null)
            {
                var userRegistrations = GetAllRegistrations();
                if (userRegistrations != null)
                {
                    using (UserRegistrationDbContext context = new UserRegistrationDbContext())
                    {
                        context.UserRegistrations.Remove(user);
                        context.SaveChanges();
                    }
                }
            }                        
        }

        public void CreateRegistration(UserRegistration user)
        {
            if (user != null)
            {
                try
                {
                    using (UserRegistrationDbContext userDbContext = new UserRegistrationDbContext())
                    {
                        userDbContext.UserRegistrations.Add(user);
                        userDbContext.SaveChanges();
                    }
                }
                catch
                {
                    string notificationText = $"Пользователь с данными:\nЛогин: {user.Name}\nПочта: {user.Email}\nСуществует в системе.";
                    NotificationView notifyView = new NotificationView(notificationText);
                    notifyView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    notifyView.ShowDialog();
                }                
            }
        }
    }
}
