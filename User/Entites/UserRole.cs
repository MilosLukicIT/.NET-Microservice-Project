using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Entites
{
    public class UserRole
    {
        [Key]
        public Guid UlogaId { get; set; }
        public string NazivUloge { get; set; }

        public ICollection<UserClass> UserClasses { get; set; }
    }
}
