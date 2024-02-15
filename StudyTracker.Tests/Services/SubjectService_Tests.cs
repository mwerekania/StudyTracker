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
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace StudyTracker.Tests
{
    [TestClass()]
    public class SubjectService_Tests
    {
        private readonly StudyTrackerDbContext _dbContext;

        public SubjectService_Tests()
        {
            var options = new DbContextOptionsBuilder<StudyTrackerDbContext>()
              .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudyTracker;Trusted_Connection=True;MultipleActiveResultSets=true")
              .Options;
            // Initialize  DbContext with a test database connection
            _dbContext = new StudyTrackerDbContext(options); // Provide connection string or options if necessary
        }

        [TestMethod()]
        public void AddSubject_Test_ValidInput_ReturnsSubjectWithId()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            var subjectName = "Math";
            var courseId = 4;

            // Act
            var addedSubject = service.AddSubject(subjectName, courseId, out string errorMessage);

            // Assert
            Assert.IsNotNull(addedSubject);
            Assert.IsTrue(addedSubject.SubjectId > 0);
        }

        [TestMethod()]
        public void AddSubject_Test_InvalidInput_ReturnsNull()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            var subjectName = "Math";
            var courseId = 0;

            // Act
            var addedSubject = service.AddSubject(subjectName, courseId, out string errorMessage);

            // Assert
            Assert.IsNull(addedSubject);
        }

        [TestMethod()]
        public void AddSubject_Test_InvalidInput_ReturnsErrorMessage()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            var subjectName = "Math";
            var courseId = 0;

            // Act
            var addedSubject = service.AddSubject(subjectName, courseId, out string errorMessage);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(errorMessage));
        }
        [TestMethod()]
        public void AddSubject_Test_RequiredFieldsCaptured()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            var subjectName = "Math";
            var courseId = 4;

            // Act
            var addedSubject = service.AddSubject(subjectName, courseId, out string errorMessage);

            // Assert
            Assert.IsNotNull(addedSubject.DateAdded);
            Assert.IsNotNull(addedSubject.SubjectName);
            Assert.IsNotNull(addedSubject.CourseId);
        }

      


        [TestMethod()]
        // Test for GetSubjectById method
        public void GetSubjectById_Test_ExistingSubjectId_ReturnsSubject()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int subjectId = 2006;

            // Act
            var subject = service.GetSubjectById(subjectId, out string errorMessage);

            // Assert
            Assert.IsNotNull(subject);
            Assert.AreEqual(subjectId, subject.SubjectId);
            Assert.AreEqual("Math", subject.SubjectName);
        }

        // Test for GetSubjectById method for a non-existing subject
        [TestMethod()]
        public void GetSubjectById_Test_NonExistingSubjectId_ReturnsNull()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int subjectId = 100;

            // Act
            var subject = service.GetSubjectById(subjectId, out string errorMessage);

            // Assert
            Assert.IsNull(subject);
        }

        // Test for GetSubject Error Message if subject does not exist
        [TestMethod()]
        public void GetSubjectById_Test_NonExistingSubjectId_ReturnsErrorMessage()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            var subjectId = -100;

            // Act
            var subject = service.GetSubjectById(subjectId, out string errorMessage);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(errorMessage));
        }


        // Test for GetAllSubjects method - returns a list of subjects
        [TestMethod]
        public void GetAllSubjects_Test_ReturnsListOfSubjects()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int courseId = 4;

            // Act
            var subjects = service.GetAllSubjects(courseId, out string errorMessage);

            // Assert
            Assert.IsNotNull(subjects);
            Assert.IsTrue(subjects.Count() > 0);
        }

        // Test for GetAllSubjects method - returns an error message if no subjects are found
        [TestMethod]
        public void GetAllSubjects_Test_NoSubjectsFound_ReturnsErrorMessage()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int courseId = 0;

            // Act
            var subjects = service.GetAllSubjects(courseId, out string errorMessage);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(errorMessage));
        }

        // Test for UpdateSubject method - updates the subject name and date modified correctly
        [TestMethod]
        public void UpdateSubject_Test_ExistingSubjectId_ReturnsUpdatedSubject()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int subjectId = 2006;
            string newSubjectName = "Chemistry";

            // Act
            var updatedSubject = service.UpdateSubject(subjectId, newSubjectName, out string errorMessage);

            // Assert
            Assert.IsNotNull(updatedSubject);
            Assert.AreEqual(subjectId, updatedSubject.SubjectId);
            Assert.AreEqual(newSubjectName, updatedSubject.SubjectName);
            Assert.IsNotNull(updatedSubject.DateModified);
            Assert.AreEqual(newSubjectName, _dbContext.Subjects.First(s => s.SubjectId == subjectId).SubjectName);
        }

        // Test for UpdateSubject method - returns null for a non-existing subject
        [TestMethod]
        public void UpdateSubject_Test_NonExistingSubjectId_ReturnsNull()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int subjectId = 100;
            string newSubjectName = "Chemistry";

            // Act
            var updatedSubject = service.UpdateSubject(subjectId, newSubjectName, out string errorMessage);

            // Assert
            Assert.IsNull(updatedSubject);
        }

        // Test for UpdateSubject method - returns an error message if the subject does not exist
        [TestMethod]
        public void UpdateSubject_Test_NonExistingSubjectId_ReturnsErrorMessage()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int nonExistingSubjectId = 9999;
            string updatedSubjectName = "Updated Subject Name";

            // Act
            var result = service.UpdateSubject(nonExistingSubjectId, updatedSubjectName, out string errorMessage);

            // Assert
            Assert.IsNull(result);
            Assert.AreEqual("Subject not found", errorMessage);
        }

        /* ------------------------------------------------------------------------------*/
        /* Tests for GetSubject method 
         * ------------------------------------------------------------------------------*/

        // Test for GetSubject method - returns a subject
        [TestMethod]
        public void GetSubject_Test_ExistingSubjectId_ReturnsSubject()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int subjectId = 2010;
            string subjectName = "Math";
            int courseId = 4;

            // Act
            var subject = service.GetSubject(subjectId, out string errorMessage);

            // Assert
            Assert.IsNotNull(subject);
            Assert.AreEqual(subjectId, subject.SubjectId);
            Assert.AreEqual(subjectName, subject.SubjectName);
            Assert.AreEqual(courseId, subject.CourseId);
        }

        // Test for GetSubject method - returns null for a non-existing subject
        [TestMethod]
        public void GetSubject_Test_NonExistingSubjectId_ReturnsNull()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int subjectId = 100;

            // Act
            var subject = service.GetSubject(subjectId, out string errorMessage);

            // Assert
            Assert.IsNull(subject);
        }

        // Test for GetSubject method - returns an error message if the subject does not exist
        [TestMethod]
        public void GetSubject_Test_NonExistingSubjectId_ReturnsErrorMessage()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int nonExistingSubjectId = 9999;

            // Act
            var result = service.GetSubject(nonExistingSubjectId, out string errorMessage);

            // Assert
            Assert.IsNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(errorMessage));
        }

        /* ------------------------------------------------------------------------------*/
        /* Tests for DeleteSubject method 
         * ------------------------------------------------------------------------------*/
        // Test for DeleteSubject method - deletes a subject
        [TestMethod]
        public void DeleteSubject_Test_ExistingSubjectId_ReturnsTrue()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int subjectId = 2006;

            // Act
            var subject = service.DeleteSubject(subjectId, out string errorMessage, out bool isSuccess);

            // Assert
            Assert.IsTrue(isSuccess);
        }

        // Test for DeleteSubject method - returns false for a non-existing subject
        [TestMethod]
        public void DeleteSubject_Test_NonExistingSubjectId_ReturnsFalse()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int subjectId = 100;

            // Act
            var result = service.DeleteSubject(subjectId, out string errorMessage, out bool isSuccess);

            // Assert
            Assert.IsFalse(isSuccess);
        }

        // Test for DeleteSubject method - returns an error message if the subject does not exist
        [TestMethod]
        public void DeleteSubject_Test_NonExistingSubjectId_ReturnsErrorMessage()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int nonExistingSubjectId = 9999;

            // Act
            var result = service.DeleteSubject(nonExistingSubjectId, out string errorMessage, out bool isSuccess);

            // Assert
            Assert.IsFalse(isSuccess);
            Assert.IsFalse(string.IsNullOrEmpty(errorMessage));
        }

        // Test for DeleteSubject method - returns an error message if the subject has already been deleted
        [TestMethod]
        public void DeleteSubject_Test_DeletedSubjectId_ReturnsErrorMessage()
        {
            // Arrange
            var service = new SubjectService(_dbContext);
            int deletedSubjectId = 2010;

            // Act
            var result = service.DeleteSubject(deletedSubjectId, out string errorMessage, out bool isSuccess);

            // Assert
            Assert.IsFalse(isSuccess);
            Assert.IsFalse(string.IsNullOrEmpty(errorMessage));
        }
    }
}