using ApplicationWithAuthorization.Model.CRUDLogic;
using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Services.Messenger
{
    public class MessengerService
    {
        public void SendMessage(Message message)
        {
            MessagesCRUD crud = new MessagesCRUD();
            crud.CreateMessage(message);
        }

        public void RemoveMessage(Message message)
        {
            MessagesCRUD crud = new MessagesCRUD();
            crud.DeleteMessage(message);
        }

        public List<Message> GetMessages()
        {
            MessagesCRUD crud = new MessagesCRUD();
            var messages = crud.ReadMessages();
            return messages;
        }
    }
}
