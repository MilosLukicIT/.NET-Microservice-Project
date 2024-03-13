using AutoMapper;
using UserMicroservice.Entites;

namespace UserMicroservice.Data
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly UserClassContext context;
        private readonly IMapper mapper;

        public UserRoleRepository (UserClassContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public List<UserRole> GetUserRoles()
        {
            return context.UserRoles.ToList();
        }

        public UserRole GetUserRoleById(Guid userRoleId)
        {
            return context.UserRoles.FirstOrDefault(e => e.UlogaId == userRoleId);
        }

        public UserRole CreateUserRole(UserRole userRole)
        {
            var createdEntity = context.Add(userRole);
            return mapper.Map<UserRole>(createdEntity.Entity);
        }

        public void DeleteUserRole(Guid userRoleId)
        {
            var userRole = GetUserRoleById(userRoleId);
            context.Remove(userRole);
        }

        public UserRole UpdateUserRole(UserRole userRole)
        {
            try
            {
                var existingUserRole = context.UserRoles.FirstOrDefault(e => e.UlogaId == userRole.UlogaId);

                if (existingUserRole != null)
                {
                    existingUserRole.NazivUloge = userRole.NazivUloge;

                    context.SaveChanges();
                    return mapper.Map<UserRole>(existingUserRole);
                }

                else
                {
                    throw new KeyNotFoundException($"User with ID {userRole.UlogaId} not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }



            return mapper.Map<UserRole>(userRole);
        }

       
    }
}
