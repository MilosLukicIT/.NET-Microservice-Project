using AutoMapper;
using System.Security.Cryptography;
using UserMicroservice.Entites;

namespace UserMicroservice.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserClassContext context;
        private readonly IMapper mapper;
        private readonly static int iterations = 1000;

        public UserRepository (UserClassContext context, IMapper mapper)
        {
            this.context = context; 
            this.mapper = mapper;
        }


        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public UserClass GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(e => e.EmailKorisnika == email);
        }

        public UserClass GetUserById(Guid userId)
        {
            return context.Users.FirstOrDefault(e => e.KorisnikId == userId);
        }

        public List<UserClass> GetUsers()
        {
            return context.Users.ToList();
        }

        public UserClass CreateUser(UserClass user)
        {

            var password = HashPassword(user.LozinkaKorisnika);
            user.KorisnikId = new Guid();
            user.LozinkaKorisnika = password.Item1;
            user.Salt = password.Item2;
            var createdEntity = context.Add(user);
            return mapper.Map<UserClass>(createdEntity.Entity);
        }

        public void DeleteUser(Guid userId)
        {
            var user = GetUserById(userId);
            context.Remove(user);
        }

        public void UpdateUser(UserClass user)
        {
            try
            {
                var existingUser = context.Users.FirstOrDefault(e => e.KorisnikId == user.KorisnikId);

                if (existingUser != null)
                {


                    context.SaveChanges();
                }

                else
                {
                    throw new KeyNotFoundException($"User with ID {user.KorisnikId} not found");
                }
            }
            catch (Exception ex) { }
            
        }

        public bool UserWithCedentialsExists(string username, string password)
        {
            UserClass user = context.Users.FirstOrDefault(e => e.EmailKorisnika == username);
            if (user == null)
            {
                return false;
            }

            if (VerifyPassword(password, user.LozinkaKorisnika, user.Salt))
            {
                return true;
            }
            return false;


        }

        private Tuple<string, string> HashPassword(string password)
        {
            var sBytes = new byte[password.Length];
            new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);
            var salt = Convert.ToBase64String(sBytes);

            var derivedBytes = new Rfc2898DeriveBytes(password, sBytes, iterations);

            return new Tuple<string, string>
            (
                Convert.ToBase64String(derivedBytes.GetBytes(256)),
                salt
            );
        }

        public bool VerifyPassword(string password, string savedHash, string savedSalt)
        {
            var saltBytes = Convert.FromBase64String(savedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedHash)
            {
                return true;
            }
            return false;
        }


    }
}
