using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyTracker.Data;
using StudyTracker.Models;
using StudyTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTracker.Tests
{
    [TestClass()]
    public class UserService_Tests
    {
        private readonly StudyTrackerDbContext _dbContext;
        private readonly StudyTrackerDbContext _dbContextFake;

        public UserService_Tests()
        {
            var options = new DbContextOptionsBuilder<StudyTrackerDbContext>()
              .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudyTracker;Trusted_Connection=True;MultipleActiveResultSets=true")
              .Options;

            var fakeOptions = new DbContextOptionsBuilder<StudyTrackerDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudyTrackerTest;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            // Initialize  DbContext with a test database connection
            _dbContextFake = new StudyTrackerDbContext(fakeOptions); // Provide connection string or options if necessary
            _dbContext = new StudyTrackerDbContext(options);
        }

        /* -------------------------------------------------------------------------------------
         *  Add User Tests
         *  ------------------------------------------------------------------------------------*/

        // Test AddUser method for Error Message when an exception is thrown
        [TestMethod()]
        public void AddUser_Test_Exception()
        {
            // Arrange
            var service = new UserService(_dbContextFake);
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = ""
            };

            string errorMessage;
            service.AddUser(user, out errorMessage);

            // Act
            var addedUser = service.AddUser(user, out errorMessage);

            // Assert
            Assert.IsNotNull(errorMessage);
            Assert.IsNull(addedUser);
        }

        // Test AddUser method for successful addition of a user
        /*
        [TestMethod()]
        public void AddUser_Test_Success()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@doe.me"
            };

            string errorMessage = "";

            // Act
            var addedUser = service.AddUser(user, out errorMessage);

            // Assert
            Assert.IsNotNull(addedUser);
            Assert.AreEqual(user.FirstName, addedUser.FirstName);
            Assert.AreEqual(user.LastName, addedUser.LastName);
            Assert.AreEqual(user.Email, addedUser.Email);
        }
        */

        /* -------------------------------------------------------------------------------------
         *   Get User Tests
         *  ------------------------------------------------------------------------------------*/

        // Test GetUserById method for a user that exists
        [TestMethod()]
        public void GetUserById_Test_UserExists()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var userId = 2;

            // Act
            var user = service.GetUserById(userId, out string errorMessage);

            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.UserId);
        }

        // Test GetUserById method for a user that does not exist
        [TestMethod()]
        public void GetUserById_Test_UserDoesNotExist()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var userId = 100;


            // Act
            var user = service.GetUserById(userId, out string errorMessage);

            // Assert
            Assert.IsNull(user);
            Assert.AreEqual("User not found!", errorMessage);
        }

        // Test GetUserById method for exception handling
        [TestMethod()]
        public void GetUserById_Test_Exception()
        {
            // Arrange
            var service = new UserService(_dbContextFake);
            var userId = 2;

            // Act
            var user = service.GetUserById(userId, out string errorMessage);

            // Assert
            Assert.IsNull(user);
        }

        // Test GetUserByEmailAndPassword method for a user that exists
        /*
        [TestMethod()]
        public void GetUserByEmailAndPassword_Test_UserExists()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var email = "james@mugambi.net";
            var password = "12345";

            // Act
            var user = service.GetUserByEmailAndPassword(email, password, out string errorMessage, out bool isSuccess);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(password, user.PasswordHash);
            Assert.IsTrue(isSuccess);
        }
        */

        // Test GetUserByEmailAndPassword method for a user that does not exist
        [TestMethod()]
        public void GetUserByEmailAndPassword_Test_UserDoesNotExist()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var email = "john@doe.me";
            var password = "12345";

            // Act
            var user = service.GetUserByEmailAndPassword(email, password, out string errorMessage, out bool isSuccess);

            // Assert
            Assert.IsNull(user);
            Assert.AreEqual("User not found!", errorMessage);
            Assert.IsFalse(isSuccess);
        }

        // Test GetUserByEmailAndPassword method for exception handling
        [TestMethod()]
        public void GetUserByEmailAndPassword_Test_Exception()
        {
            // Arrange
            var service = new UserService(_dbContextFake);
            var email = "";
            var password = "";

            // Act
            var user = service.GetUserByEmailAndPassword(email, password, out string errorMessage, out bool isSuccess);

            // Assert
            Assert.IsNull(user);
            Assert.IsNotNull(errorMessage);
            Assert.IsFalse(isSuccess);
        }


        /* -------------------------------------------------------------------------------------
         *  Update User Tests
         * ------------------------------------------------------------------------------------*/
        // Test UpdateUser method for a user that does not exist
        [TestMethod()]
        public void UpdateUser_Test_UserDoesNotExist()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var user = new User
            {
                UserId = 1,
                FirstName = "James",
                LastName = "Mugambi",
                Email = ""
            };

            // Act
            var updatedUser = service.UpdateUser(user, out string errorMessage);

            // Assert
            Assert.IsNull(updatedUser);
            Assert.AreEqual("User not found!", errorMessage);
        }

        // Test UpdateUser method for a user that exists
        [TestMethod()]
        public void UpdateUser_Test_UserExists()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var user = new User
            {
                UserId = 2,
                FirstName = "James",
                LastName = "Mugambi",
                Email = "james@mugambi.me",
                UserName = "james"

            };

            // Act
            var updatedUser = service.UpdateUser(user, out string errorMessage);

            // Assert
            Assert.IsNotNull(updatedUser);
        }

        // Test UpdateUser method for exception handling
        [TestMethod()]
        public void UpdateUser_Test_Exception()
        {
            // Arrange
            var service = new UserService(_dbContextFake);
            var user = new User
            {
                UserId = 2,
                FirstName = "James",
                LastName = "Mugambi",
                Email = ""
            };

            // Act
            var updatedUser = service.UpdateUser(user, out string errorMessage);

            // Assert
            Assert.IsNull(updatedUser);
            Assert.IsNotNull(errorMessage);
        }

        // Test UpdateUser method for a user that exists and is changing email
        [TestMethod()]
        public void UpdateUser_Test_ChangeEmail()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var user = new User
            {
                UserId = 1002,
                FirstName = "Kabon",
                LastName = "Kredit",
                Email = "Kabon@credits.me"
            };

            // Act
            var updatedUser = service.UpdateUser(user, out string errorMessage);

            // Assert
            Assert.IsNull(updatedUser);
            Assert.AreEqual("Email already exists!", errorMessage);
        }

        // Test UpdateUser method for a user that exists and is changing username
        [TestMethod()]
        public void UpdateUser_Test_ChangeUserName()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var user = new User
            {
                UserId = 1003,
                FirstName = "Kabon",
                LastName = "Kredit",
                UserName =  "joe"
            };

            // Act
            var updatedUser = service.UpdateUser(user, out string errorMessage);

            // Assert
            Assert.IsNull(updatedUser);
            Assert.AreEqual("Username already exists!", errorMessage);
        }

        /* -------------------------------------------------------------------------------------
         *  Verify User Tests
         *  ------------------------------------------------------------------------------------*/
        // Test VerifyUser method for a user that exists
        /*
        [TestMethod()]
        public void VerifyUser_Test_UserExists()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var userId = 5;

            // Act
            var verifiedUser = service.VerifyUser(userId, out string errorMessage);

            // Assert
            Assert.IsNotNull(verifiedUser);
        }
        */
        // Test VerifyUser method for a user that does not exist
        [TestMethod()]
        public void VerifyUser_Test_UserDoesNotExist()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var userId = 100;

            // Act
            var verifiedUser = service.VerifyUser(userId, out string errorMessage);

            // Assert
            Assert.IsNull(verifiedUser);
            Assert.AreEqual("User not found!", errorMessage);
        }

        // Test VerifyUser method for exception handling
        [TestMethod()]
        public void VerifyUser_Test_Exception()
        {
            // Arrange
            var service = new UserService(_dbContextFake);
            var userId = 5;

            // Act
            var verifiedUser = service.VerifyUser(userId, out string errorMessage);

            // Assert
            Assert.IsNull(verifiedUser);
            Assert.IsNotNull(errorMessage);
        }

        // Test VerifyUser method for a user that is already verified
        [TestMethod()]
        public void VerifyUser_Test_UserAlreadyVerified()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var userId = 2;

            // Act
            var verifiedUser = service.VerifyUser(userId, out string errorMessage);

            // Assert
            Assert.IsNull(verifiedUser);
            Assert.AreEqual("User already verified!", errorMessage);
        }

        /* -------------------------------------------------------------------------------------
         *         *  Change Password Tests
         *  ------------------------------------------------------------------------------------*/
        // Test ChangePassword method for a user that exists
        [TestMethod()]
        public void ChangePassword_Test_UserExists()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var userId = 5;
            var newPassword = "12345";

            // Act
            var changedPasswordUser = service.ChangePassword(userId, newPassword, out string errorMessage);

            // Assert
            Assert.IsNotNull(changedPasswordUser);
        }

        // Test ChangePassword method for a user that does not exist
        [TestMethod()]
        public void ChangePassword_Test_UserDoesNotExist()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var userId = 100;
            var newPassword = "12345";

            // Act
            var changedPasswordUser = service.ChangePassword(userId, newPassword, out string errorMessage);

            // Assert
            Assert.IsNull(changedPasswordUser);
            Assert.AreEqual("User not found!", errorMessage);
        }

        // Test ChangePassword method for exception handling
        [TestMethod()]
        public void ChangePassword_Test_Exception()
        {
            // Arrange
            var service = new UserService(_dbContextFake);
            var userId = 5;
            var newPassword = "12345";

            // Act
            var changedPasswordUser = service.ChangePassword(userId, newPassword, out string errorMessage);

            // Assert
            Assert.IsNull(changedPasswordUser);
            Assert.IsNotNull(errorMessage);
        }

        /* -------------------------------------------------------------------------------------
         *         *  Delete User Tests
         * ------------------------------------------------------------------------------------*/
        // Test DeleteUser method for a user that exists
        [TestMethod()]
        public void DeleteUser_Test_UserExists()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var userId = 5;

            // Act
            var isDeleted = service.DeleteUser(userId, out string errorMessage);

            // Assert
            Assert.IsTrue(isDeleted);

        }

        // Test DeleteUser method for a user that does not exist
        [TestMethod()]
        public void DeleteUser_Test_UserDoesNotExist()
        {
            // Arrange
            var service = new UserService(_dbContext);
            var userId = 100;

            // Act
            var isDeleted = service.DeleteUser(userId, out string errorMessage);

            // Assert
            Assert.IsFalse(isDeleted);
            Assert.AreEqual("User not found!", errorMessage);
        }

        // Test DeleteUser method for exception handling
        [TestMethod()]
        public void DeleteUser_Test_Exception()
        {
            // Arrange
            var service = new UserService(_dbContextFake);
            var userId = 5;

            // Act
            var isDeleted = service.DeleteUser(userId, out string errorMessage);

            // Assert
            Assert.IsFalse(isDeleted);
            Assert.IsNotNull(errorMessage);
        }

    }
}