namespace Generalizacija.Entity
{
    public class TimskiSastanak : Sastanak
    {
        public string? razlog {  get; set; }
        public Guid? Tim {  get; set; }
    }
}
