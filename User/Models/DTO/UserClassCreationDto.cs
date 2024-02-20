using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Models.DTO
{
    public class UserClassCreationDto
    {
        [Required]
        public string ImeKorisnika { get; set; }
        [Required]
        public string PrezimeKorisnika { get; set; }
        [Required]
        public string EmailKorisnika { get; set; }
        [Required]
        public string LozinkaKorisnika { get; set; }
        public string KontaktKorisnika { get; set; }
        [Required]
        public DateOnly DatumRegistracije { get; set; }
        public string? Salt { get; set; }

        [DefaultValue(true)]
        public bool PrvoLogovanje { get; set; }
        public Guid? TimId { get; set; }
        public Guid? KalendarId { get; set; }
        public Guid? UlogaId { get; set; }
    }
}
