using Generalizacija.Dto;
using Generalizacija.Entity;

namespace Generalizacija.Data
{
    public interface ISastanakRepository
    {
        public List<SastanakInfoDto> GetAll();

    }
}
