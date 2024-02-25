using StudyTracker.Models;

namespace StudyTracker.Tests
{
    [TestClass]
    public class Assignment_Tests
    {
        [TestMethod]
        public void Assignment_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int assignmentId = 1;
            string title = "Math Assignment";
            int subjectId = 1;
            var userId = "2";
            DateTime dueDate = DateTime.Now.AddDays(7);
            Priority priority = Priority.High;
            string description = "Solve problems from chapters 1 to 5.";
            DateTime completionDate = DateTime.Now.AddDays(7);

            // Act
            Assignment assignment = new Assignment
            {
                AssignmentId = assignmentId,
                Title = title,
                SubjectId = subjectId,
                AppUserID = userId,
                DueDate = dueDate,
                Priority = priority,
                Description = description,
                CompletionDate = completionDate
            };

            // Assert
            Assert.AreEqual(assignmentId, assignment.AssignmentId);
            Assert.AreEqual(title, assignment.Title);
            Assert.AreEqual(subjectId, assignment.SubjectId);
            Assert.AreEqual(userId, assignment.AppUserID);
            Assert.AreEqual(dueDate, assignment.DueDate);
            Assert.AreEqual(priority, assignment.Priority);
            Assert.AreEqual(description, assignment.Description);
            Assert.AreEqual(completionDate, assignment.CompletionDate);
        }

        [TestMethod]
        public void Assignment_DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            Assignment assignment = new Assignment();

            // Act
            int defaultSubjectId = 0;
            string defaultTitle = string.Empty;
            string defaultDescription = string.Empty;
            var defaultUserId = string.Empty;
            Priority defaultPriority = Priority.Low;
            Status defaultStatus = Status.NotStarted;

            // Assert
            Assert.AreEqual(defaultSubjectId, assignment.SubjectId);
            Assert.AreEqual(defaultTitle, assignment.Title);
            Assert.AreEqual(defaultDescription, assignment.Description);
            Assert.AreEqual(defaultUserId, assignment.AppUserID);
            Assert.AreEqual(defaultPriority, assignment.Priority);
            Assert.AreEqual(defaultStatus, assignment.Status);
        }

        [TestMethod]
        public void Assignment_DefaultConstructor_SetsDateAdded()
        {
            // Arrange
            Assignment assignment = new Assignment();

            // Act
            DateTime dateAdded = DateTime.Now;

            // Assert
            Assert.AreEqual(dateAdded.Date, assignment.DateAdded.Date);
        }

        [TestMethod]
        // Test the Priority enum
        public void Priority_Enum_HasThreeOptions()
        {
            // Arrange
            int expectedCount = 4;

            // Act
            int actualCount = Enum.GetNames(typeof(Priority)).Length;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        // Test the Status enum
        public void Status_Enum_HasThreeOptions()
        {
            // Arrange
            int expectedCount = 3;

            // Act
            int actualCount = Enum.GetNames(typeof(Status)).Length;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
