using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationWithAuthorization.Model.Database.Entities
{
    [Table("Messages")]
    public class Message
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string? Text { get; set; }
    }
}
