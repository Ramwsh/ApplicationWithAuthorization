using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Model.Prototype
{
    public class UserPrototype : IPrototype
    {
        private User userClone;
        
        public IPrototype Clone()
        {            
            UserPrototype clone = new UserPrototype();
            clone.userClone = userClone;
            return clone;
        }

        public UserPrototype SetUserModel(User user)
        {
            userClone = user;
            return this;
        }        

        public User CopyClone()
        {
            return userClone;
        }
    }
}
