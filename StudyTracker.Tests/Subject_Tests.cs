using StudyTracker.Models;

namespace StudyTracker.Tests
{
    [TestClass]
    public class Subject_Tests
    {
        [TestMethod]
        public void Subject_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int subjectId = 1;
            int courseId = 1;
            string subjectName = "Algebra";
            DateTime dateAdded = DateTime.Now;

            // Act
            Subject subject = new()
            {
                SubjectId = subjectId,
                CourseId = courseId,
                SubjectName = subjectName,
                DateAdded = dateAdded,
            };

            // Assert
            Assert.AreEqual(subjectId, subject.SubjectId);
            Assert.AreEqual(courseId, subject.CourseId);
            Assert.AreEqual(subjectName, subject.SubjectName);
            Assert.AreEqual(dateAdded, subject.DateAdded);
        }
    }
}
