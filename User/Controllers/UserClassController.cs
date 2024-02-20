using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Data;
using UserMicroservice.Entites;
using UserMicroservice.Models.DTO;

namespace UserMicroservice.Controllers
{

    [ApiController]
    [Route("api/korisnik")]
    [Produces("application/json", "application/xml")]
    public class UserClassController : Controller
    {

        private readonly IUserRepository userRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;



        public UserClassController(IUserRepository userRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<List<UserClassDto>> GetAllUsers()
        {
            var users = userRepository.GetUsers();

            if (users == null || users.Count == 0) {
                
                return NoContent();
            }

            return Ok(mapper.Map<List<UserClassDto>>(users));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserClassDto> GetUserById(Guid id) 
        {
            var user = userRepository.GetUserById(id);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<UserClassDto>(user));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<UserClassDto> CreateUser([FromBody] UserClassCreationDto userCreationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                UserClass mappedUser = mapper.Map<UserClass>(userCreationDto);
                UserClass createdUser = userRepository.CreateUser(mappedUser);
                userRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetUser", "UserClass", new { korisnikId = createdUser.KorisnikId });

                return Created(location, mapper.Map<UserClassDto>(createdUser));
            }
            catch {

                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }




        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                var user = userRepository.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }

                userRepository.DeleteUser(id);
                userRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }


    }
}
