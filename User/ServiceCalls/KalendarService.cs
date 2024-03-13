using Newtonsoft.Json;
using UserMicroservice.Models.DTO;

namespace UserMicroservice.ServiceCalls
{
    public class KalendarService : IKalendarService
    {

        private readonly IConfiguration configuration;
        private readonly ILoggerService logger;

        public KalendarService(IConfiguration configuration, ILoggerService logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        public void createKalendar(KalendarDto kalendar)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var x = configuration["Services:KalendarService"];
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(kalendar));
                    content.Headers.ContentType.MediaType = "application/json";

                    HttpResponseMessage response = httpClient.PostAsync(x, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        logger.Log(LogLevel.Information, "CreateCalendarService", "Successfully created calendar!");
                    }
                    else
                    {
                        logger.Log(LogLevel.Warning, "CreateCalendarService", "Didn't create calendar!");
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
}
