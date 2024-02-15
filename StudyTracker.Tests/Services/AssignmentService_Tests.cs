using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using StudyTracker.Data;
using StudyTracker.Models;
using StudyTracker.Services;
using Microsoft.VisualBasic;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;


namespace StudyTracker.Tests
{
    [TestClass]
    public class AssignmentService_Tests
    {
        private readonly StudyTrackerDbContext _dbContext;
        private readonly StudyTrackerDbContext _dbContextFake;

        public AssignmentService_Tests()
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

        [TestMethod]
        public void AddAssignment_WithPriorityAndStatus_SuccessfullyAdded()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            Assignment assignment = new Assignment
            {
                Title = "Test Assignment 3",
                Description = "Test Assignment Description 3",
                Priority = Priority.High,
                Status = Status.Completed,
                DueDate = DateTime.Now.AddDays(7),
                CompletionDate = DateTime.Now,
                UserId = 2,
                SubjectId = 2013
            };

            // Act
            var result = service.AddAssignment(assignment, out string errorMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", errorMessage);
            // Additional assertions can be added to verify the correctness of the assignment properties
        }


        // Test AddAssignment method with null priority and status
        [TestMethod]
        public void AddAssignment_WithNullPriorityAndStatus_SuccessfullyAdded()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            Assignment assignment = new Assignment
            {
                Title = "Test Assignment 4",
                Description = "Test Assignment Description 4",
                Priority = Priority.None,
                Status = Status.NotStarted,
                DueDate = null,
                CompletionDate = null,
                UserId = 2,
                SubjectId = 2013
            };

            // Act
            var result = service.AddAssignment(assignment, out string errorMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", errorMessage);
            Assert.AreEqual(Priority.None, result.Priority);
            Assert.AreEqual(Status.NotStarted, result.Status);
            Assert.IsNotNull(result.DateAdded);
            // Additional assertions can be added to verify the correctness of the assignment properties
        }

        /*--------------------------------------------------------------------------------------------------------
         * Test GetAssignmentById
         * ------------------------------------------------------------------------------------------------------*/

        // Test GetAssignmentById method for invalid AssignmentId

        [TestMethod]
        public void GetAssignmentById_InvalidAssignmentId_ReturnsNull()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            int assignmentId = 1000;

            // Act
            var result = service.GetAssignmentById(assignmentId, out string errorMessage);

            // Assert
            Assert.IsNull(result);
            Assert.AreEqual("Assignment not found", errorMessage);
        }

        // Test GetAssignmentById method for valid AssignmentId
        [TestMethod]
        public void GetAssignmentById_ValidAssignmentId_ReturnsAssignment()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            int assignmentId = 4;

            // Act
            var result = service.GetAssignmentById(assignmentId, out string errorMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", errorMessage);
            Assert.AreEqual(assignmentId, result.AssignmentId);
            // Additional assertions can be added to verify the correctness of the assignment properties
        }

        /*--------------------------------------------------------------------------------------------------------
         * Test GetAllAssignmentsBySubjectId
         * ------------------------------------------------------------------------------------------------------*/

        // Test GetAllAssignmentsBySubjectId method for invalid SubjectId
        [TestMethod]
        public void GetAllAssignmentsBySubjectId_InvalidSubjectId_ReturnsEmptyList()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            int subjectId = 1000;

            // Act
            var result = service.GetAllAssignmentsBySubjectId(subjectId, out string errorMessage);

            // Assert
            Assert.IsNull(result);
            Assert.AreEqual("Subject not found", errorMessage);
        }

        // Test GetAllAssignmentsBySubjectId method for valid SubjectId
        [TestMethod]
        public void GetAllAssignmentsBySubjectId_ValidSubjectId_ReturnsAssignments()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            int subjectId = 2013;

            // Act
            var result = service.GetAllAssignmentsBySubjectId(subjectId, out string errorMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", errorMessage);
            // Additional assertions can be added to verify the correctness of the assignment properties
        }

        // Test GetAllAssignmentsBySubjectId method for valid SubjectId with no assignments
        [TestMethod]
        public void GetAllAssignmentsBySubjectId_ValidSubjectIdNoAssignments_ReturnsEmptyList()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            int subjectId = 2016;

            // Act
            var result = service.GetAllAssignmentsBySubjectId(subjectId, out string errorMessage);

            // Assert
            Assert.IsNull(result);
            Assert.AreEqual("No assignments found", errorMessage);
        }

        // Test GetAllAssignmentsBySubjectId method for Error Message when an exception is thrown
        [TestMethod]
        public void GetAllAssignmentsBySubjectId_ExceptionThrown_ReturnsNull()
        {
            // Arrange
            var service = new AssignmentService(_dbContextFake);
            int subjectId = 2016;

            // Act
            var result = service.GetAllAssignmentsBySubjectId(subjectId, out string errorMessage);

            // Assert
            Assert.IsNull(result);
            Assert.AreNotEqual("", errorMessage);
        }

        /*--------------------------------------------------------------------------------------------------------
        * Test GetAllAssignmentsByUserId
        * ------------------------------------------------------------------------------------------------------*/
        // Test GetAllAssignmentsByUserId method for valid UserId
        [TestMethod]
        public void GetAllAssignmentsByUserId_ValidUserId_ReturnsAssignments()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            int userId = 2;

            // Act
            var result = service.GetAllAssignmentsByUserId(userId, out string errorMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", errorMessage);        }

        // Test GetAllAssignmentsByUserId method for valid UserId with no assignments
        [TestMethod]
        public void GetAllAssignmentsByUserId_ValidUserIdNoAssignments_ReturnsEmptyList()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            int userId = 5;

            // Act
            var result = service.GetAllAssignmentsByUserId(userId, out string errorMessage);

            // Assert
            Assert.IsNull(result);
            Assert.AreEqual("No assignments found", errorMessage);
        }

        // Test GetAllAssignmentsByUserId method for Error Message when an exception is thrown
        [TestMethod]
        public void GetAllAssignmentsByUserId_ExceptionThrown_ReturnsNull()
        {
            // Arrange
            var service = new AssignmentService(_dbContextFake);
            int userId = 3;

            // Act
            var result = service.GetAllAssignmentsByUserId(userId, out string errorMessage);

            // Assert
            Assert.IsNull(result);
            Assert.AreNotEqual("", errorMessage);
        }

        // Test GetAllAssignmentsByUserId method for invalid UserId
        [TestMethod]
        public void GetAllAssignmentsByUserId_InvalidUserId_ReturnsNull()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            int userId = 1000;

            // Act
            var result = service.GetAllAssignmentsByUserId(userId, out string errorMessage);

            // Assert
            Assert.IsNull(result);
            Assert.AreEqual("User not found", errorMessage);
        }

        /*--------------------------------------------------------------------------------------------------------
         *  Test UpdateAssignment
         * ------------------------------------------------------------------------------------------------------*/
        // Test UpdateAssignment method for invalid AssignmentId
        [TestMethod]
        public void UpdateAssignment_InvalidAssignmentId_ReturnsNull()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            Assignment assignment = new Assignment
            {
                AssignmentId = 0,
                Title = "Test Assignment 2",
                Description = "Test Assignment Description 2",
                Priority = Priority.High,
                Status = Status.Completed,
                DueDate = DateTime.Now.AddDays(7),
                CompletionDate = DateTime.Now,
                UserId = 2,
                SubjectId = 2013
            };

            // Act
            var result = service.UpdateAssignment(assignment, out string errorMessage);

            // Assert
            Assert.IsNull(result);
            Assert.AreEqual("Assignment not found", errorMessage);
        }

        // Test UpdateAssignment method for valid AssignmentId
        [TestMethod]
        public void UpdateAssignment_ValidAssignmentId_ReturnsUpdatedAssignment()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            Assignment assignment = new Assignment
            {
               AssignmentId = 5,
               Title = "Test Assignment 2",
               Description = "Test Assignment Description 2",
               Priority = Priority.High,
               Status = Status.Completed,
               DueDate = DateTime.Now.AddDays(7),
               CompletionDate = DateTime.Now,
               UserId = 2,
               SubjectId = 2013
            };

            // Act
            var result = service.UpdateAssignment(assignment, out string errorMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", errorMessage);
            Assert.AreEqual(assignment.AssignmentId, result.AssignmentId);
            Assert.IsNotNull(result.DateModified);

        }

        // Test UpdateAssignment method for Error Message when an exception is thrown
        [TestMethod]
        public void UpdateAssignment_ExceptionThrown_ReturnsNull()
        {
            // Arrange
            var service = new AssignmentService(_dbContextFake);
            Assignment assignment = new Assignment
            {
                AssignmentId = 5,
                Title = "Test Assignment 2",
                Description = "Test Assignment Description 2",
                Priority = Priority.High,
                Status = Status.Completed,
                DueDate = DateTime.Now.AddDays(7),
                CompletionDate = DateTime.Now,
                UserId = 2,
                SubjectId = 2013
            };

            // Act
            var result = service.UpdateAssignment(assignment, out string errorMessage);

            // Assert
            Assert.IsNull(result);
            Assert.AreNotEqual("", errorMessage);
        }

        /* --------------------------------------------------------------------------------------------------------
         *  Test DeleteAssignment
         * ------------------------------------------------------------------------------------------------------*/

        // Test DeleteAssignment method for invalid AssignmentId
        [TestMethod]
        public void DeleteAssignment_InvalidAssignmentId_ReturnsNull()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            int assignmentId = 0;

            // Act
            var result = service.DeleteAssignment(assignmentId, out string errorMessage, out bool isSuccess);

            // Assert
            Assert.IsNull(result);
            Assert.AreEqual("Assignment not found", errorMessage);
            Assert.IsFalse(isSuccess);
        }

        // Test DeleteAssignment method for valid AssignmentId
        [TestMethod]
        public void DeleteAssignment_ValidAssignmentId_ReturnsDeletedAssignment()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            int assignmentId = 7;

            // Act
            var result = service.DeleteAssignment(assignmentId, out string errorMessage, out bool isSuccess);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", errorMessage);
            Assert.IsTrue(isSuccess);
            Assert.AreEqual(assignmentId, result.AssignmentId);
            Assert.IsNotNull(result.DateDeleted);
        }

        // Test DeleteAssignment method for Error Message when an exception is thrown
        [TestMethod]
        public void DeleteAssignment_ExceptionThrown_ReturnsNull()
        {
            // Arrange
            var service = new AssignmentService(_dbContextFake);
            int assignmentId = 6;

            // Act
            var result = service.DeleteAssignment(assignmentId, out string errorMessage, out bool isSuccess);

            // Assert
            Assert.IsNull(result);
            Assert.AreNotEqual("", errorMessage);
            Assert.IsFalse(isSuccess);
        }

        // Test DeleteAssignment method for Error Message when an assignment is completed
        [TestMethod]
        public void DeleteAssignment_AssignmentCompleted_ReturnsNull()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            int assignmentId = 5;

            // Act
            var result = service.DeleteAssignment(assignmentId, out string errorMessage, out bool isSuccess);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Assignment is completed and cannot be deleted", errorMessage);
            Assert.IsFalse(isSuccess);
        }

        // Test DeleteAssignment method for Error Message when an assignment is already deleted
        [TestMethod]
        public void DeleteAssignment_AssignmentAlreadyDeleted_ReturnsNull()
        {
            // Arrange
            var service = new AssignmentService(_dbContext);
            int assignmentId = 7;

            // Act
            var result = service.DeleteAssignment(assignmentId, out string errorMessage, out bool isSuccess);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Assignment already deleted", errorMessage);
            Assert.IsFalse(isSuccess);
        }

        
    }
}