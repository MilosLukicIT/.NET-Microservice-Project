using Microsoft.EntityFrameworkCore;

namespace Generalizacija.Entity
{
    public class GeneralizacijaContext : DbContext
    {

        private readonly IConfiguration config;
        public GeneralizacijaContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            this.config = config;
        }


        public DbSet<Sastanak> Sastanaks { get; set;}
        public DbSet<IndividualniSastanak> individualniSastanaks { get; set;}
        public DbSet<TimskiSastanak> timskiSastanaks { get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("KalendarDB"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sastanak>()
                .HasDiscriminator(s => s.sastanak_type)
                .HasValue<Sastanak>("Sastanak")
                .HasValue<IndividualniSastanak>("individualni")
                .HasValue<TimskiSastanak>("timski"); 

            modelBuilder.Entity<Sastanak>()
                .Property(s => s.sastanak_type)
                .HasColumnName("Tip sastanka");

            modelBuilder.Entity<IndividualniSastanak>();
            modelBuilder.Entity<TimskiSastanak>();

        }
    }
}
