using Generalizacija.Entity;

namespace Generalizacija.Data
{
    public interface ITimskiSastanakRepository
    {
        public TimskiSastanak CreateInd(TimskiSastanak sastanak);
        public List<TimskiSastanak> GetAll();

        public bool SaveChanges();

    }
}
