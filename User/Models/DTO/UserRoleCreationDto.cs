using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Models.DTO
{
    public class UserRoleCreationDto
    {
        [Required]
        public string NazivUloge { get; set; }
    }
}
