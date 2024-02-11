using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;

namespace UserMicroservice.Entites
{
    public class UserClassContext : DbContext
    {
        private readonly IConfiguration configuration;


        public UserClassContext (DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<UserClass> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserDB"));
            
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var password = HashPassword("lozinka");

            modelBuilder.Entity<UserClass>().HasIndex(u => u.EmailKorisnika).IsUnique();

            modelBuilder.Entity<UserRole>().
                HasMany(e => e.UserClasses).
                WithOne(e => e.Uloga).
                HasForeignKey(e => e.UlogaId).
                IsRequired(false);

            modelBuilder.Entity<UserClass>()
                .HasData(new
                {
                    KorisnikId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ImeKorisnika = "Milos",
                    PrezimeKorisnika = "Lukic",
                    EmailKorisnika = "milos@mail.com",
                    LozinkaKorisnika = password.Item1,
                    KontaktKorisnika = "065777888",
                    DatumRegistracije = DateOnly.Parse("2020-11-15"),
                    Salt = password.Item2,
                    PrvoLogovanje = true,
                    UlogaId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c397478"),
                    TimId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c399586"),
                    KalendarId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c399700")
                }
                )
                ;

            modelBuilder.Entity<UserRole>()
                .HasData(new
                {
                    UlogaId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c397478"),
                    NazivUloge = "Admin"
                });

            modelBuilder.Entity<UserRole>()
                .HasData(new
                {
                    UlogaId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c397123"),
                    NazivUloge = "Product owner"
                });

            modelBuilder.Entity<UserRole>()
                .HasData(new
                {
                    UlogaId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c397456"),
                    NazivUloge = "Developer"
                });

            modelBuilder.Entity<UserRole>()
                .HasData(new
                {
                    UlogaId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c397222"),
                    NazivUloge = "Scrum master"
                });
        }

        private Tuple<string, string> HashPassword(string password)
        {
            var sBytes = new byte[password.Length];
            new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);
            var salt = Convert.ToBase64String(sBytes);

            var derivedBytes = new Rfc2898DeriveBytes(password, sBytes, 1000);

            return new Tuple<string, string>
            (
                Convert.ToBase64String(derivedBytes.GetBytes(256)),
                salt
            );
        }


    }
}
