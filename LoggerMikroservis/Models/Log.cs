namespace LoggerMikroservis.Models
{
    public class Log
    {
        public LogLevel LogLevel { get; set; }

        public string ServiceName { get; set; }

        public string Message { get; set; }

        public string Method { get; set; }

        public Exception Exception { get; set; }
    }
}
