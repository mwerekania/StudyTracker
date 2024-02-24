using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using StudyTracker.Models;
using StudyTracker.Services;
using Moq;
using StudyTracker.Data;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace StudyTracker.Tests
{
    [TestClass()]
    public class CourseService_Tests
    {
        private readonly StudyTrackerDbContext _dbContext;
        private readonly StudyTrackerDbContext _dbContextFake;

        public CourseService_Tests()
        {
            var options = new DbContextOptionsBuilder<StudyTrackerDbContext>()
               .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudyTracker;Trusted_Connection=True;MultipleActiveResultSets=true")
               .Options;

            var fakeOptions = new DbContextOptionsBuilder<StudyTrackerDbContext>()
               .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudyTrackerTest;Trusted_Connection=True;MultipleActiveResultSets=true")
               .Options;

            // Initialize  DbContext with a test database connection
            _dbContext = new StudyTrackerDbContext(options); // Provide connection string or options if necessary
            _dbContextFake = new StudyTrackerDbContext(fakeOptions); //
        }

        /*--------------------------------------------------------------------------------
         *  Tests for GetCoursesByUserIDAsync method
         *  ---------------------------------------------------------------------------------
         */


        // Test method to verify that the GetCoursesByUserIDAsync method returns the correct courses for a user
        [TestMethod]
        public void GetCoursesByUserIDAsync_ExistingUserId_ReturnsCourses()
        {
            // Arrange
            var service = new CourseService(_dbContext);
            string userId = "2";

            // Act
            var courses = service.GetCoursesByUserIDAsync(userId).Result;

            // Assert
            Assert.IsNotNull(courses);
            Assert.IsTrue(courses.Count > 0);
            foreach (var course in courses)
            {
                Assert.AreEqual(userId, course.AppUserID);
            }
        }



        [TestMethod()]
        public void AddCourse_Test()
        {
            // Arrange
            var service = new CourseService(_dbContext);
            var courseName = "Geography";
            var userId = "5";


            // Act
            var addedCourse = service.AddCourse(courseName, userId);

            // Assert
            Assert.IsNotNull(addedCourse);
            Assert.AreEqual(courseName, addedCourse.CourseName);
            Assert.AreEqual(userId, addedCourse.AppUserID);
            Assert.IsNotNull(addedCourse.DateAdded);
        }

        // test of GetCoursesByUserIDAsync method exists
        [TestMethod()]
        public void GetCoursesByUserIDAsync_Exists_Test()
        {
            // Arrange
            var mockCourseService = new Mock<CourseService>();

            // Act
            var methodInfo = typeof(CourseService).GetMethod("GetCoursesByUserIDAsync");

            // Assert
            Assert.IsNotNull(methodInfo);
        }

        // test GetCoursesByUserIDAsync method returns Task
        [TestMethod()]
        public void GetCoursesByUserIDAsync_ReturnsTask_Test()
        {
            // Arrange
            var mockCourseService = new Mock<CourseService>();

            // Act
            var methodInfo = typeof(CourseService).GetMethod("GetCoursesByUserIDAsync");

            // Assert
            Assert.AreEqual(typeof(Task<IList<Course>>), methodInfo.ReturnType);
        }

        // test GetCoursesByUserIDAsync method returns Course list
        [TestMethod()]
        public void GetCoursesByUserIDAsync_ReturnsCourseList_Test()
        {
            // Arrange
            var mockCourseService = new Mock<CourseService>();

            // Act
            var methodInfo = typeof(CourseService).GetMethod("GetCoursesByUserIDAsync");

            // Assert
            Assert.AreEqual(typeof(IList<Course>), methodInfo.ReturnType.GetGenericArguments()[0]);
        }

        // test GetCoursesByUserIDAsync method returns Course list for userID
        [TestMethod()]
        public void GetCoursesByUserIDAsync_ReturnsCourseListForUserID_Test()
        {
            // Arrange
            var mockCourseService = new Mock<CourseService>();

            // Act
            var methodInfo = typeof(CourseService).GetMethod("GetCoursesByUserIDAsync");

            // Assert
            Assert.IsNotNull(methodInfo.GetParameters());
            Assert.AreEqual(1, methodInfo.GetParameters().Length);
            Assert.AreEqual(typeof(int?), methodInfo.GetParameters()[0].ParameterType);
        }
 

        // test GetCoursesByUserIDAsync method accepts userId as parameter  
        [TestMethod()]
        public void GetCoursesByUserIDAsync_AcceptsUserId_Test()
        {
            // Arrange
            var mockCourseService = new Mock<CourseService>();

            // Act
            var methodInfo = typeof(CourseService).GetMethod("GetCoursesByUserIDAsync");

            // Assert
            Assert.IsNotNull(methodInfo.GetParameters());
            Assert.AreEqual(1, methodInfo.GetParameters().Length);
            Assert.AreEqual(typeof(int?), methodInfo.GetParameters()[0].ParameterType);
        }




        // Test method to verify that the GetCourseById method returns the correct course
        [TestMethod]
        public void GetCourseById_ExistingCourseId_ReturnsCourse()
        {
            // Arrange
            var service = new CourseService(_dbContext);
            int courseId = 5;

            // Act
            var course = service.GetCourseById(courseId);

            // Assert
            Assert.IsNotNull(course);
            Assert.AreEqual(courseId, course.CourseId);
            Assert.AreEqual("Economics", course.CourseName);
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