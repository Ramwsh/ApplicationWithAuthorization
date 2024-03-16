using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Model.CRUDLogic
{
    interface IRegCRUD
    {
        List<UserRegistration> GetAllRegistrations();
        public void RemoveRegistration(UserRegistration user);
        public void CreateRegistration(UserRegistration user);
    }
}
