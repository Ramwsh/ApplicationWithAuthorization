using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationWithAuthorization.Model.Database.Entities
{
    [Table("UserLogins")]
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
