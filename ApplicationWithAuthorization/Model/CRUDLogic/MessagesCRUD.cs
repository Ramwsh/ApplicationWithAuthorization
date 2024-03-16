using ApplicationWithAuthorization.Model.Database.Entities;
using ApplicationWithAuthorization.Model.Database.Contexts;

namespace ApplicationWithAuthorization.Model.CRUDLogic
{
    public class MessagesCRUD
    {
        public void CreateMessage(Message message)
        {
            using (MessagesDbContext context = new MessagesDbContext())
            {
                context.Messages.Add(message);
                context.SaveChanges();
            }
        }

        public List<Message> ReadMessages()
        {
            using (MessagesDbContext context = new MessagesDbContext())
            {
                var messages = context.Messages.ToList();
                return messages;
            }
        }

        public void DeleteMessage(Message message)
        {
            using (MessagesDbContext context = new MessagesDbContext())
            {
                context.Messages.Remove(message);
                context.SaveChanges();
            }
        }
    }
}
