using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using UserMicroservice.Data;
using UserMicroservice.Entites;
using UserMicroservice.Models.DTO;

namespace UserMicroservice.Controllers
{

    [ApiController]
    [Route("api/korisnik/uloga")]
    [Produces("application/json", "application/xml")]
    public class UserRoleController : Controller
    {

        private readonly IUserRoleRepository userRoleRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;


        public UserRoleController(LinkGenerator linkGenerator, IMapper mapper, IUserRoleRepository userRoleRepository)
        {
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.userRoleRepository = userRoleRepository;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<List<UserRoleDto>> GetAllUserRoles()
        {
            var userRoles = userRoleRepository.GetUserRoles();

            if (userRoles == null || userRoles.Count == 0)
            {

                return NoContent();
            }

            return Ok(mapper.Map<List<UserRoleDto>>(userRoles));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserRoleDto> GetUserById(Guid id)
        {
            var userRole = userRoleRepository.GetUserRoleById(id);

            if (userRole == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<UserRoleDto>(userRole));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<UserRoleDto> CreateUser([FromBody] UserRoleCreationDto userRoleCreationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                UserRole mappedUserRole = mapper.Map<UserRole>(userRoleCreationDto);
                UserRole createdUserRole = userRoleRepository.CreateUserRole(mappedUserRole);
                userRoleRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetUserRole", "UserRole", new { ulogaId = createdUserRole.UlogaId });

                return Created(location, mapper.Map<UserRoleDto>(createdUserRole));
            }
            catch
            {

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
                var userRole = userRoleRepository.GetUserRoleById(id);

                if (userRole == null)
                {
                    return NotFound();
                }

                userRoleRepository.DeleteUserRole(id);
                userRoleRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

    }
}
