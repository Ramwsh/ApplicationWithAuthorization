using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationWithAuthorization.Model.Database.Entities
{
    [Table("UserChangeCards")]
    public class UserChangeCard
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? NewName { get; set; }
        public string? NewPassword { get; set; }
        public string? NewEmail { get; set; }
    }
}
