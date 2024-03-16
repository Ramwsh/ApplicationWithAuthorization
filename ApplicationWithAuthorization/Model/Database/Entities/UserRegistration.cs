using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationWithAuthorization.Model.Database.Entities
{
    [Table("UserRegistrations")]    
    public class UserRegistration
    {
        public string? Name { get; set; }

        [Key]
        public string? Email { get; set; }
        public string? VerificationCode { get; set; }
    }
}
