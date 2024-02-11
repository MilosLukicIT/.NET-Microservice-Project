using UserMicroservice.Entites;

namespace UserMicroservice.Models.DTO
{
    public class UserClassDto
    {
        public Guid KorisnikId { get; set; }
        public string ImeKorisnika { get; set; }
        public string PrezimeKorisnika { get; set; }
        public string EmailKorisnika { get; set; }
        public string LozinkaKorisnika { get; set; }
        public string KontaktKorisnika { get; set; }
        public DateOnly DatumRegistracije { get; set; }
        public string Salt { get; set; }
        public bool PrvoLogovanje { get; set; }
        public Guid TimId { get; set; }
        public Guid KalendarId { get; set; }
        public Guid UlogaId { get; set; }
    }
}
