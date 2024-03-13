using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Models.DTO
{
    public class UserClassUpdateDto
    {
        
        public Guid KorisnikId { get; set; }
        public string ImeKorisnika { get; set; }
        public string PrezimeKorisnika { get; set; }
        public string EmailKorisnika { get; set; }
        public string? LozinkaKorisnika { get; set; }
        public string KontaktKorisnika { get; set; }
        public bool PrvoLogovanje { get; set; }
        public Guid? TimId { get; set; }
        public Guid UlogaId { get; set; }
    }

}
