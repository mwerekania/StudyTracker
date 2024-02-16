using StudyTracker.Data;
using StudyTracker.Models;

namespace StudyTracker.Services
{
    public class UserService
    {
        private readonly StudyTrackerDbContext _dbcontext;

        public UserService(StudyTrackerDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public User? AddUser(User user, out string errorMessage)
        {
            try
            {
                // Check if email already exists
                var userWithEmail = _dbcontext.Users.FirstOrDefault(u => u.Email == user.Email);
                if (userWithEmail != null)
                {
                    errorMessage = "Email already exists!";
                    return null;
                }
                // Check if username already exists
                var userWithUserName = _dbcontext.Users.FirstOrDefault(u => u.UserName == user.UserName);
                if (userWithUserName != null)
                {
                    errorMessage = "Username already exists!";
                    return null;
                }
                _dbcontext.Users.Add(user);
                _dbcontext.SaveChanges();
                errorMessage = "";
                return user;
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public User? GetUserById(int userId, out string errorMessage)
        {
            try
            {
                var user  = _dbcontext.Users.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    errorMessage = "User not found!";
                    return null;
                }
                else
                {
                    errorMessage = "";
                    return user;
                }
                
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        // Get user by email and password
        public User? GetUserByEmailAndPassword(string email, string password, out string errorMessage, out bool isSuccess)
        {
            try
            {
                var user = _dbcontext.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
                if (user == null)
                {
                    errorMessage = "User not found!";
                    isSuccess = false;
                    return null;
                }
                else
                {
                    errorMessage = "";
                    isSuccess = true;
                    return user;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                isSuccess = false;
                return null;
            }
        }

        // Update user
        public User? UpdateUser(User user, out string errorMessage)
        { 
            try
            {
                // check if user exists
                var userOnFile = _dbcontext.Users.FirstOrDefault(u => u.UserId == user.UserId);
                if (userOnFile == null)
                {
                    errorMessage = "User not found!";
                    return null;
                }
                else
                {
                    // Check if user is changing email
                    if (userOnFile.Email != user.Email)
                    {
                        var userWithEmail = _dbcontext.Users.FirstOrDefault(u => u.Email == user.Email);
                        if (userWithEmail != null)
                        {
                            errorMessage = "Email already exists!";
                            return null;
                        }
                    }

                    // Check if user is changing username
                    if (userOnFile.UserName != user.UserName)
                    {
                        var userWithUserName = _dbcontext.Users.FirstOrDefault(u => u.UserName == user.UserName);
                        if (userWithUserName != null)
                        {
                            errorMessage = "Username already exists!";
                            return null;
                        }
                    }

                    // only update particular fields
                    userOnFile.FirstName = user.FirstName;
                    userOnFile.LastName = user.LastName;
                    userOnFile.Email = user.Email;
                    userOnFile.UserName = user.UserName;
                    userOnFile.DateModified = System.DateTime.Now;

                    _dbcontext.Users.Update(userOnFile);
                    _dbcontext.SaveChanges();
                    errorMessage = "";
                    return user;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        // Verify user
        public User? VerifyUser(int userId, out string errorMessage)
        {
            try
            {
                var user = _dbcontext.Users.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    errorMessage = "User not found!";
                    return null;
                }
                else
                {
                    user.IsVerified = true;
                    _dbcontext.Users.Update(user);
                    _dbcontext.SaveChanges();
                    errorMessage = "";
                    return user;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        // Change password
        public User? ChangePassword(int userId, string newPassword, out string errorMessage)
        {
            try
            {
                var user = _dbcontext.Users.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    errorMessage = "User not found!";
                    return null;
                }
                else
                {
                    user.PasswordHash = newPassword;
                    _dbcontext.Users.Update(user);
                    _dbcontext.SaveChanges();
                    errorMessage = "";
                    return user;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        // Delete user
        public bool DeleteUser(int userId, out string errorMessage)
        {
            try
            {
                var user = _dbcontext.Users.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    errorMessage = "User not found!";
                    return false;
                }
                else
                {
                    user.DateDeleted = DateTime.Now;
                    _dbcontext.SaveChanges();
                    errorMessage = "";
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
