using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Data;
using UserMicroservice.Entites;
using UserMicroservice.Models.DTO;
using UserMicroservice.ServiceCalls;

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
        private readonly ILoggerService logger;
        private readonly IKalendarService kalendarService;



        public UserClassController(IUserRepository userRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService logger, IKalendarService kalendarService)
        {
            this.userRepository = userRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.logger = logger;
            this.kalendarService = kalendarService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<List<UserClassViewDto>> GetAllUsers()
        {
            List<UserClassViewDto> users = userRepository.GetUsers();

            if (users == null || users.Count == 0) {

                logger.Log( LogLevel.Warning, "GetAllUsers", "No users to return");
                return NoContent();
            }

            logger.Log( LogLevel.Information, "GetAllUsers", "Returned all users");
            return Ok(users);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserClassViewDto> GetUserById(Guid id) 
        {
            UserClassViewDto user = userRepository.GetUserById(id);

            if(user == null)
            {
                logger.Log( LogLevel.Warning, "GetUserById", "User does not exist");
                return NotFound();
            }
            logger.Log( LogLevel.Information, "GetUserById", "Returned user with id:" + id);
            return Ok(user);
        }

        
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public ActionResult<UserClassViewDto> CreateUser([FromBody] UserClassCreationDto userCreationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.Log( LogLevel.Warning, "CreateUser", "Model state error");
                    return BadRequest(ModelState);
                }

                UserClass mappedUser = mapper.Map<UserClass>(userCreationDto);
                UserClass createdUser = userRepository.CreateUser(mappedUser);
                userRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetUser", "UserClass", new { korisnikId = createdUser.KorisnikId });

                logger.Log( LogLevel.Information, "CreateUser", "Created user with id: " + createdUser.KorisnikId.ToString());
                 KalendarDto kalendar = new KalendarDto();
                if(createdUser != null)
                {
                    kalendar.KorisnikId = createdUser.KorisnikId;
                    kalendar.KalendarskaGodina = DateTime.Now.Year.ToString();
                    kalendarService.createKalendar(kalendar);
                }
                
                return Created(location, mapper.Map<UserClassViewDto>(createdUser));
            }
            catch (Exception ex) {

                logger.Log( LogLevel.Error, "CreateUser", "Exception: " + ex.ToString());
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
                    logger.Log( LogLevel.Warning, "DeleteUser", "User does not exist");
                    return NotFound();
                }

                userRepository.DeleteUser(id);
                userRepository.SaveChanges();
                logger.Log( LogLevel.Information, "DeleteUser", "Deleted user with id: " + id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.Log( LogLevel.Error, "DeleteUser", "Didn't delete user. Exception: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType (StatusCodes.Status500InternalServerError)]

        public ActionResult <UserClassViewDto> UpdateUser(Guid id, [FromBody]UserClassUpdateDto user)
        {
            try
            {
                var existUser = userRepository.GetUserById(id);

                if (existUser == null)
                {
                    logger.Log( LogLevel.Warning, "UpdateUser", "User does not exist");
                    return NotFound();
                }
                user.KorisnikId = id;
                UserClass mappedUser = mapper.Map<UserClass>(user);
                var updatedUser = userRepository.UpdateUser(mappedUser);
                logger.Log( LogLevel.Information, "UpdateUser", "Updated user with id: " + id);
                return Ok(mapper.Map<UserClassViewDto>(updatedUser));

            }
            catch(Exception ex) {
                logger.Log( LogLevel.Error, "UpdateUser", "Didn't update user. Exception: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
                }
        }

    }
}
