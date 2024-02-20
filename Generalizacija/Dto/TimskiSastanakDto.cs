namespace Generalizacija.Dto
{
    public class TimskiSastanakDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string? razlog { get; set; }
        public Guid? Tim { get; set; }
    }
}
