using UserMicroservice.Entites;

namespace UserMicroservice.Data
{
    public interface IUserRepository
    {

        List<UserClass> GetUsers();
        UserClass GetUserById(Guid userId);
        UserClass GetUserByEmail(string email);
        UserClass CreateUser(UserClass user);
        void UpdateUser (UserClass user);
        void DeleteUser (Guid userId);
        public bool UserWithCedentialsExists(string username, string password);
        public bool SaveChanges ();

    }
}
