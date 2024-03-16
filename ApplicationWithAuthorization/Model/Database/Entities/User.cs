using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationWithAuthorization.Model.Database.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]        
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? Remind { get; set; }
        public string? PasswordDate { get; set; }
        public string? PasswordHistory { get; set; }
    }
}
