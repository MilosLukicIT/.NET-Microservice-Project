using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using UserMicroservice.Data;
using UserMicroservice.Entites;
using UserMicroservice.Models.DTO;
using UserMicroservice.ServiceCalls;

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
        private readonly ILoggerService loggerService;


        public UserRoleController(LinkGenerator linkGenerator, IMapper mapper, IUserRoleRepository userRoleRepository, ILoggerService loggerService)
        {
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.userRoleRepository = userRoleRepository;
            this.loggerService = loggerService;
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<List<UserRoleDto>> GetAllUserRoles()
        {
            var userRoles = userRoleRepository.GetUserRoles();

            if (userRoles == null || userRoles.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAllUserRoles", "No user roles to return");
                return NoContent();
            }
            loggerService.Log(LogLevel.Information, "GetAllUserRoles", "Successfully returned all roles!");
            return Ok(mapper.Map<List<UserRoleDto>>(userRoles));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserRoleDto> GetUserRoleById(Guid id)
        {
            var userRole = userRoleRepository.GetUserRoleById(id);

            if (userRole == null)
            {
                loggerService.Log(LogLevel.Warning, "GetUserRoleById", "No user roles to return");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetUserRoleById", "Returned user role with id: " + id);
            return Ok(mapper.Map<UserRoleDto>(userRole));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<UserRoleDto> CreateUserRole([FromBody] UserRoleCreationDto userRoleCreationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    loggerService.Log(LogLevel.Warning, "CreateUserRole", "Model state error");
                    return BadRequest(ModelState);
                }

                UserRole mappedUserRole = mapper.Map<UserRole>(userRoleCreationDto);
                UserRole createdUserRole = userRoleRepository.CreateUserRole(mappedUserRole);
                userRoleRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetUserRole", "UserRole", new { ulogaId = createdUserRole.UlogaId });
                loggerService.Log(LogLevel.Information, "CreateUserRole", "Created user role with id: " + createdUserRole.UlogaId.ToString());
                return Created(location, mapper.Map<UserRoleDto>(createdUserRole));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "CreateUserRole", "Exception: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUserRole(Guid id)
        {
            try
            {
                var userRole = userRoleRepository.GetUserRoleById(id);

                if (userRole == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteUserRole", "No user role to return");
                    return NotFound();
                }

                userRoleRepository.DeleteUserRole(id);
                userRoleRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteUserRole", "Deleted user role with id: " + id);
                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "DeleteUserRole", "Didn't delete user role. Exception: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<UserRoleDto> UpdateUserRole(Guid id, [FromBody] UserRoleUpdateDto userRole)
        {
            try
            {
                var existUserRole = userRoleRepository.GetUserRoleById(id);

                if (existUserRole == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateUserRole", "No user role to return");
                    return NotFound();
                }

                userRole.UlogaId = id;
                UserRole mappedUser = mapper.Map<UserRole>(userRole);
                var updatedUserRole = userRoleRepository.UpdateUserRole(mappedUser);
                loggerService.Log(LogLevel.Information, "UpdateUserRole", "Updated user role with id: " + id);
                return Ok(mapper.Map<UserRoleDto>(updatedUserRole)); ;

            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "UpdateUserRole", "Didn't update user role. Exception: " + ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
