using UserMicroservice.Models.DTO;

namespace UserMicroservice.ServiceCalls
{
    public interface ILoggerService
    {
        void Log(LogLevel level, string method, string message,  Exception exception = null);
    }
}
