using UserMicroservice.Models.DTO;

namespace UserMicroservice.ServiceCalls
{
    public interface IKalendarService
    {
        public void createKalendar(KalendarDto kalendar);
    }
}
