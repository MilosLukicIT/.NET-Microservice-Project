using Newtonsoft.Json;
using UserMicroservice.Models.DTO;

namespace UserMicroservice.ServiceCalls
{
    public class LoggerService : ILoggerService
    {

        private readonly IConfiguration configuration;

        public LoggerService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Log(LogLevel level, string method, string message, Exception? exception = null)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var x = configuration["Services:LoggerService"];

                    var log = new LoggerDto
                    {
                        LogLevel = level,
                        Method = method,
                        Message = message,
                        Exception = exception ?? new Exception("Unknown error"),
                        ServiceName = "UserService"
                    };

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(log));
                    content.Headers.ContentType.MediaType = "application/json";

                    HttpResponseMessage response = httpClient.PostAsync(x, content).Result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
