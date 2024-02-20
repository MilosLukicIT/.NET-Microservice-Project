using Generalizacija.Entity;

namespace Generalizacija.Data
{
    public interface IIndSastanakRepository
    {
        public IndividualniSastanak CreateInd(IndividualniSastanak sastanak);
        public List<IndividualniSastanak> GetAll();

        public bool SaveChanges();

    }
}
