using WebDevCrafts.DbConnections;
using WebDevCrafts.Helpers;
using WebDevCrafts.Models.Entities;

namespace WebDevCrafts.Services
{
    public class UserService
    {
        private readonly AppDbContext db;

        public UserService(AppDbContext db)
        {
            this.db = db;
        }

        public List<User> GetAllUsers()
        {
            var users = db.Users.Where(u => !u.IsSoftDeleted).ToList();
            return users;
        }

        public bool AddUser(UserModel userModel)
        {
            var isUserExist = db.Users.Any(u => u.Email == userModel.Email);

            if (isUserExist)
            {
                return false;
            }

            var newUser = new User
            {
                Name = userModel.Name,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber
            };

            db.Users.Add(newUser);
            return db.SaveChanges() > 0;
        }

        public bool RemoveUser(string userId)
        {
            var user = db.Users.Where(u => u.Id == Guid.Parse(userId)).FirstOrDefault();

            if(user != null)
            {
                user.IsSoftDeleted = true;
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
