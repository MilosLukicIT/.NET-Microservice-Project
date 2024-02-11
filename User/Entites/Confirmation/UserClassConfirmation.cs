namespace UserMicroservice.Entites.Confirmation
{
    public class UserClassConfirmation
    {
        public Guid KorisnikId { get; set; }
        public string ImeKorsnika { get; set; }
        public string PrezimeKorisnika { get; set; }
        public string EmailKorsnika { get; set; }
        public string? KontaktKorisnika { get; set; }
        public DateOnly DatumRegistracije { get; set; }
        public UserRole? Uloga { get; set; }
    }
}
