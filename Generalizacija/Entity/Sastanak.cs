using System.ComponentModel.DataAnnotations;

namespace Generalizacija.Entity
{
    public class Sastanak
    {

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string? sastanak_type { get; set; }
    }
}
