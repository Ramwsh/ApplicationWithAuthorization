using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.Model.Database.Contexts;

namespace ApplicationWithAuthorization.Model.CRUDLogic
{
    public class UserChangeCardCRUD
    {
        public void CreateUserChangeCard(UserChangeCard card)
        {
            if (card != null)
            {
                using (UserChangeCardDbContext context = new UserChangeCardDbContext())
                {
                    context.UserChangeCards.Add(card);                    
                    context.SaveChanges();
                }
            }            
        }

        public List<UserChangeCard> ReadUserChangeCards()
        {
            using (UserChangeCardDbContext context = new UserChangeCardDbContext())
            {
                return context.UserChangeCards.ToList();
            }
        }

        public void UpdateUserChangeCard(UserChangeCard card, UserChangeCard updatedCard)
        {
            if (card != null && updatedCard != null)
            {
                using (UserChangeCardDbContext context = new UserChangeCardDbContext())
                {
                    UserChangeCard cardToUpdate = context.UserChangeCards.Find(card.Id);
                    if (cardToUpdate != null)
                    {
                        cardToUpdate.NewEmail = updatedCard.NewEmail;
                        cardToUpdate.NewPassword = updatedCard.NewPassword;
                        cardToUpdate.NewName = updatedCard.NewName;
                        cardToUpdate.Name = updatedCard.Name;
                        cardToUpdate.Email = updatedCard.Email;
                        cardToUpdate.Password = updatedCard.Password;
                        context.SaveChanges();
                    }                    
                }
            }
        }

        public void DeleteUserChangeCard(UserChangeCard card)
        {
            if (card != null)
            {
                using (UserChangeCardDbContext context = new UserChangeCardDbContext())
                {
                    context.UserChangeCards.Remove(card);
                    context.SaveChanges();
                }
            }
        }
    }
}
