using StudyTracker.Models;

namespace StudyTracker.Tests
{
    [TestClass]
    public class Course_Tests
    {
        [TestMethod]
        public void Course_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int courseId = 1;
            string courseName = "Math";
            string userId = "12";
            DateTime dateAdded = DateTime.Now;

            // Act
            Course course = new()
            {
                CourseId = courseId,
                CourseName = courseName,
                AppUserID = userId,
                DateAdded = dateAdded,
            };

            // Assert
            Assert.AreEqual(courseId, course.CourseId);
            Assert.AreEqual(courseName, course.CourseName);
            Assert.AreEqual(userId, course.AppUserID);
            Assert.AreEqual(dateAdded, course.DateAdded);
        }
    }
}
