using AutoMapper;
using Generalizacija.Dto;
using Generalizacija.Entity;

namespace Generalizacija.Data
{
    public class SastanakRepository : ISastanakRepository
    {

        public readonly GeneralizacijaContext generalizacijaContext;
        public readonly IMapper mapper;

        public SastanakRepository( GeneralizacijaContext generalizacijaContext, IMapper mapper) {
        
            this.generalizacijaContext = generalizacijaContext;
            this.mapper = mapper;
        }
        public List<SastanakInfoDto> GetAll()
        {
            List<SastanakInfoDto> indSastanak = mapper.Map<List<SastanakInfoDto>>(generalizacijaContext.individualniSastanaks.ToList());
            indSastanak.AddRange(mapper.Map<List<SastanakInfoDto>>(generalizacijaContext.timskiSastanaks.ToList()));
            return mapper.Map<List<SastanakInfoDto>>(indSastanak);
        }
    }
}
