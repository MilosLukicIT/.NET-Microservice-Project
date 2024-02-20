using AutoMapper;
using Generalizacija.Entity;

namespace Generalizacija.Data
{
    public class IndSastanakRepository : IIndSastanakRepository
    {

        public readonly GeneralizacijaContext generalizacijaContext;
        private readonly IMapper mapper;

        public IndSastanakRepository(GeneralizacijaContext generalizacijaContext, IMapper mapper) {
        
            this.generalizacijaContext = generalizacijaContext;
            this.mapper = mapper;
        }

        public IndividualniSastanak CreateInd(IndividualniSastanak s)
        {
            var created = generalizacijaContext.Add(s);
            return mapper.Map<IndividualniSastanak>(created.Entity);
        }

        public List<IndividualniSastanak> GetAll()
        {
            return generalizacijaContext.individualniSastanaks.ToList();
        }

        public bool SaveChanges()
        {
            return generalizacijaContext.SaveChanges() > 0;
        }
    }
}
