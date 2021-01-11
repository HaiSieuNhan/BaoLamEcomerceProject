using Ecommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Domain;
using System.Linq;
using Ecommerce.Repository.Interfaces;
using EcommerceCommon.Infrastructure.Helper;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = await GetFirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
            // check if username exists
            if (user == null)
                return null;
            // check if password is correct
            if (!AuthenUserHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public Task<User> RegisterUser(User user)
        {
            throw new NotImplementedException();
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (DbContext.Users.Any(x => x.Username == user.Username))
                throw new Exception("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash;
            byte[] passwordSalt;

            AuthenUserHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            DbContext.Users.Add(user);
            DbContext.SaveChanges();

            return user;
        }

        public void Update(User userParam, string password = null)
        {
            var user = DbContext.Users.Find(userParam.Id);

            if (user == null)
                throw new Exception("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (DbContext.Users.Any(x => x.Username == userParam.Username))
                    throw new Exception("Username " + userParam.Username + " is already taken");
            }

            // update user properties

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                AuthenUserHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            DbContext.Users.Update(user);
            DbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = DbContext.Users.Find(id);
            if (user != null)
            {
                DbContext.Users.Remove(user);
                DbContext.SaveChanges();
            }
        }

    }
}
