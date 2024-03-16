using ApplicationWithAuthorization.Model.CRUDLogic;
using ApplicationWithAuthorization.Model.Database.Entities;

namespace ApplicationWithAuthorization.Services.Messenger
{
    public class MessageIdentifier
    {
        private MessengerService service;

        public MessageIdentifier()
        {
            service = new MessengerService();
        }

        public User IdentifySenderByMessage(Message message)
        {
            var messages = service.GetMessages();
            UserCRUD crud = new UserCRUD();
            var senderUser = crud.ReadUsers().Where(user => user.Id == message.FromUserId).FirstOrDefault();
            return senderUser;
        }

        public User IdentifyRecipientByMessage(Message message)
        {
            var messages = service.GetMessages();
            UserCRUD crud = new UserCRUD();
            var recipientUser = crud.ReadUsers().Where(user => user.Id == message.ToUserId).FirstOrDefault();
            return recipientUser;
        }

        public List<Message> IdentifyMessagesByRecipient(User recipientUser)
        {
            var messages = service.GetMessages();
            var recipientMessages = messages.Select(message => message).Where(message => message.ToUserId == recipientUser.Id).ToList();
            return recipientMessages;
        }
    }
}
