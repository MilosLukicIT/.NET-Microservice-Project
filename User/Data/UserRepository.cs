using AutoMapper;
using System.Security.Cryptography;
using UserMicroservice.Entites;
using UserMicroservice.Models.DTO;

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

        public UserClassViewDto GetUserById(Guid userId)
        {
            UserClassViewDto user = mapper.Map<UserClassViewDto>(context.Users.FirstOrDefault(e => e.KorisnikId == userId));
            user.NazivUloge = context.UserRoles.FirstOrDefault(e => e.UlogaId == user.UlogaId).NazivUloge;
            return user;
        }

        public List<UserClassViewDto> GetUsers()
        {
            List<UserClassViewDto> users = mapper.Map<List<UserClassViewDto>>(context.Users.ToList());
            for (int i = 0; i < users.Count; i++)
            {
                users[i].NazivUloge = context.UserRoles.FirstOrDefault(e => e.UlogaId == users[i].UlogaId).NazivUloge;
            }
            return users;
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
            UserClass user = context.Users.FirstOrDefault(e => e.KorisnikId == userId);
            context.Remove(user);
        }

        public UserClass UpdateUser(UserClass user)
        {
            try
            {
                var existingUser = context.Users.FirstOrDefault(e => e.KorisnikId == user.KorisnikId);

                if (existingUser != null)
                {
                    if (user.LozinkaKorisnika != null && VerifyPassword(user.LozinkaKorisnika, existingUser.LozinkaKorisnika, existingUser.Salt) == false) 
                    {
                        Tuple<string, string> newPassword = HashPassword(user.LozinkaKorisnika);
                        existingUser.LozinkaKorisnika = newPassword.Item1;
                        existingUser.Salt = newPassword.Item2;

                    }

                    existingUser.KorisnikId = user.KorisnikId;
                    existingUser.ImeKorisnika = user.ImeKorisnika;
                    existingUser.PrezimeKorisnika = user.PrezimeKorisnika;
                    existingUser.EmailKorisnika = user.EmailKorisnika;
                    existingUser.KontaktKorisnika = user.KontaktKorisnika;
                    existingUser.PrvoLogovanje = user.PrvoLogovanje;
                    existingUser.TimId = user.TimId;
                    existingUser.UlogaId = user.UlogaId;
                    
                    context.SaveChanges();
                    return mapper.Map<UserClass>(existingUser);
                }

                else
                {
                    throw new KeyNotFoundException($"User with ID {user.KorisnikId} not found");
                }
            }
            catch (Exception ex) {
                throw new Exception("Error", ex);
            }
            
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
