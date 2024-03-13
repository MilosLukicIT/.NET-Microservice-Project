using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserMicroservice.Entites
{
    public class UserClass
    {

        [Key]
        public Guid KorisnikId { get; set; }
        public string ImeKorisnika { get; set; }
        public string PrezimeKorisnika { get; set; }
        public string EmailKorisnika { get; set; }
        public string LozinkaKorisnika { get; set; }
        public string? KontaktKorisnika { get; set; }
        public DateOnly DatumRegistracije { get; set; }
        public string Salt {  get; set; }
        public bool PrvoLogovanje { get; set; }

        public Guid? TimId { get; set; }

        
        public Guid UlogaId { get; set; }
        public UserRole? Uloga {  get; set; }
    }
}
