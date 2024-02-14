using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using StudyTracker.Models;
using StudyTracker.Services;
using Moq;
using StudyTracker.Data;

namespace StudyTracker.Tests
{
    [TestClass()]
    public class CourseService_Tests
    {
        private readonly StudyTrackerDbContext _dbContext;

        public CourseService_Tests()
        {
            var options = new DbContextOptionsBuilder<StudyTrackerDbContext>()
               .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudyTracker;Trusted_Connection=True;MultipleActiveResultSets=true")
               .Options;
            // Initialize  DbContext with a test database connection
            _dbContext = new StudyTrackerDbContext(options); // Provide connection string or options if necessary
        }


        [TestMethod()]
        public void AddCourse_Test()
        {
            // Arrange
            var service = new CourseService(_dbContext);
            var courseName = "Geography";
            var userId = 5;


            // Act
            var addedCourse = service.AddCourse(courseName, userId);

            // Assert
            Assert.IsNotNull(addedCourse);
            Assert.AreEqual(courseName, addedCourse.CourseName);
            Assert.AreEqual(userId, addedCourse.UserId);
            Assert.IsNotNull(addedCourse.DateAdded);
        }
        /*
        [TestInitialize]
        public void TestInitialize()
        {


            // Optionally, you can add some sample data to the test database here
            _dbContext.Courses.AddRange(new[]
            {
            new Course { CourseName = "Math", UserId = 2 },
            new Course { CourseName = "Science", UserId = 2 },
            // Add more sample courses as needed
            });

            _dbContext.SaveChanges();
        }
        */


        // Test method to verify that the GetCourseById method returns the correct course
        [TestMethod]
        public void GetCourseById_ExistingCourseId_ReturnsCourse()
        {
            // Arrange
            var service = new CourseService(_dbContext);
            int courseId = 4;

            // Act
            var course = service.GetCourseById(courseId);

            // Assert
            Assert.IsNotNull(course);
            Assert.AreEqual(courseId, course.CourseId);
            Assert.AreEqual("Mathematics", course.CourseName);
            // Add more assertions as needed to verify other properties of the course
        }

        // Test method to verify that the GetCourseById method returns null for a non-existing course
        [TestMethod]
        public void GetCourseById_NonExistingCourseId_ReturnsNull()
        {
            // Arrange
            var service = new CourseService(_dbContext);
            int courseId = 100;

            // Act
            var course = service.GetCourseById(courseId);

            // Assert
            Assert.IsNull(course);
        }

        // Test method to verify that the UpdateCourse method updates the course name and date modified correctly
        [TestMethod]
        public void UpdateCourse_ExistingCourseId_ReturnsUpdatedCourse()
        {
            // Arrange
            var service = new CourseService(_dbContext);
            int courseId = 9;
            string newCourseName = "Chemistry";

            // Act
            var updatedCourse = service.UpdateCourse(courseId, newCourseName);

            // Assert
            Assert.IsNotNull(updatedCourse);
            Assert.AreEqual(courseId, updatedCourse.CourseId);
            Assert.AreEqual(newCourseName, updatedCourse.CourseName);
            Assert.IsNotNull(updatedCourse.DateModified);
            Assert.AreEqual(newCourseName, _dbContext.Courses.First(c => c.CourseId == courseId).CourseName);
        }

        [TestMethod]
        public void UpdateCourse_NonExistingCourseId_ReturnsNull()
        {
            // Arrange
            var service = new CourseService(_dbContext);
            int courseId = 100; // Assuming there is no course with this ID in the test database
            string newCourseName = "New Course Name";

            // Act
            var updatedCourse = service.UpdateCourse(courseId, newCourseName);

            // Assert
            Assert.IsNull(updatedCourse);
        }

        [TestMethod]
        public void DeleteCourse_ExistingCourseId_ReturnsTrueAndMarksCourseAsDeleted()
        {
            // Arrange
            var service = new CourseService(_dbContext);
            int courseIdToDelete = 7;

            // Act
            var result = service.DeleteCourse(courseIdToDelete);

            // Assert
            Assert.IsTrue(result);
            var deletedCourse = _dbContext.Courses.FirstOrDefault(c => c.CourseId == courseIdToDelete);
            Assert.IsNotNull(deletedCourse);
            Assert.IsNotNull(deletedCourse.DateDeleted);
        }

        [TestMethod]
        public void DeleteCourse_NonExistingCourseId_ReturnsFalseAndDoesNotMarkCourseAsDeleted()
        {
            // Arrange
            var service = new CourseService(_dbContext);
            int nonExistingCourseId = 100;

            // Act
            var result = service.DeleteCourse(nonExistingCourseId);

            // Assert
            Assert.IsFalse(result);
            var nonDeletedCourse = _dbContext.Courses.FirstOrDefault(c => c.CourseId == nonExistingCourseId);
            Assert.IsNull(nonDeletedCourse); // Ensure the course was not marked as deleted
        }

        [TestMethod]
        public void GetAllCourses_ReturnsAllCoursesFromDatabase()
        {
            // Arrange
            var service = new CourseService(_dbContext);
            
            var expectedRecordCount = _dbContext.Courses.Count();

            // Act
            var actualCourses = service.GetAllCourses();
    
            // Assert
            Assert.AreEqual(expectedRecordCount, actualCourses.Count());
        }
    }
}