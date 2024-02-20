namespace Generalizacija.Dto
{
    public class SastanakInfoDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string? nesto { get; set; }
        public string? razlog { get; set; }
        public Guid? Tim { get; set; }
        public string sastanak_type { get; set; }

    }
}
