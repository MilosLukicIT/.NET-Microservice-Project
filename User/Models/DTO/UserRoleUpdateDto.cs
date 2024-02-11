using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Models.DTO
{
    public class UserRoleUpdateDto
    {

        [Required]
        public Guid UlogaId { get; set; }

        [Required]
        public string NazivUloge { get; set; }
    }
}
