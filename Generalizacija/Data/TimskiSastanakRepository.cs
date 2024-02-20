using AutoMapper;
using Generalizacija.Entity;

namespace Generalizacija.Data
{
    public class TimskiSastanakRepository : ITimskiSastanakRepository
    {

        public readonly GeneralizacijaContext generalizacijaContext;
        private readonly IMapper mapper;

        public TimskiSastanakRepository(GeneralizacijaContext generalizacijaContext, IMapper mapper) {
        
            this.generalizacijaContext = generalizacijaContext;
            this.mapper = mapper;
        }

        public TimskiSastanak CreateInd(TimskiSastanak s)
        {
            var created = generalizacijaContext.Add(s);
            return mapper.Map<TimskiSastanak>(created.Entity);
        }

        public List<TimskiSastanak> GetAll()
        {
            return generalizacijaContext.timskiSastanaks.ToList();
        }

        public bool SaveChanges()
        {
            return generalizacijaContext.SaveChanges() > 0;
        }
    }
}
