using StudyTracker.Models;

namespace StudyTracker.Tests
{
    [TestClass]
    public class User_Tests
    {
        [TestMethod]
        public void User_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int userId = 1;
            string firstName = "John";
            string lastName = "Doe";
            string email = "";
            string password = "password";
            string fullName = "John Doe";

            // Act
            User user = new()
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = password
            };

            // Assert
            Assert.AreEqual(userId, user.UserId);
            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(password, user.PasswordHash);
            Assert.AreEqual(fullName, user.FullName());
        }

    }
}
