using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Models.DTO
{
    public class UserClassUpdateDto
    {
        public Guid KorisnikId { get; set; }

        [Required]
        public string ImeKorsnika { get; set; }
        [Required]
        public string PrezimeKorisnika { get; set; }
        [Required]
        public string EmailKorisnika { get; set; }
        public string KontaktKorisnika { get; set; }
        public DateOnly DatumRegistracije { get; set; }
        public bool PrvoLogovanje { get; set; }
        public Guid UlogaId { get; set; }
        public Guid TimId { get; set; }
    }
}
