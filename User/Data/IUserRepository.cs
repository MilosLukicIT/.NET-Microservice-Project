using UserMicroservice.Entites;
using UserMicroservice.Models.DTO;

namespace UserMicroservice.Data
{
    public interface IUserRepository
    {

        List<UserClassViewDto> GetUsers();
        UserClassViewDto GetUserById(Guid userId);
        UserClass GetUserByEmail(string email);
        UserClass CreateUser(UserClass user);
        UserClass UpdateUser (UserClass user);
        void DeleteUser (Guid userId);
        public bool UserWithCedentialsExists(string username, string password);
        public bool SaveChanges ();

    }
}
