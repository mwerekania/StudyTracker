using Microsoft.EntityFrameworkCore;
using StudyTracker.Data;
using StudyTracker.Models;

namespace StudyTracker.Services
{
    public class SubjectService
    {
        private readonly StudyTrackerDbContext _dbcontext;

        public SubjectService(StudyTrackerDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public Subject AddSubject(string subjectName, int courseId, string userId, out string errorMessage)
        {
            Subject subject = new Subject
            {
                SubjectName = subjectName,
                CourseId = courseId,
                DateAdded = DateTime.Now,
                AppUserID = userId
            };

            try
            {
                _dbcontext.Subjects.Add(subject);
                _dbcontext.SaveChanges();
                errorMessage = ""; // No error message as operation succeeded
                return subject;
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message; // Set error message to exception message
                return null;
            }
        }

        // Get a subject by its ID
        public Subject GetSubjectById(int subjectId, out string errorMessage)
        {
            try
            {
                Subject subject = _dbcontext.Subjects
                    .Include(s => s.Course)
                    .FirstOrDefault(s => s.SubjectId == subjectId);

                errorMessage = "";
                if (subject == null)
                {
                    errorMessage = "Subject not found";
                    return null;
                }
                else
                {
                    errorMessage = "";
                    return subject;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        // Get all subjects by User ID
        public IList<Subject> GetSubjectsWithCoursesAsync(string userId, out string errorMessage)
        {
            try
            {
                IList<Subject> subjects = _dbcontext.Subjects
                    .Include(s => s.Course)
                    .Where(s => s.Course.AppUserID == userId && s.DateDeleted == null)
                    .ToList();

                if (subjects.Count() == 0)
                {
                    errorMessage = "No subjects found";
                    return null;
                }
                else
                {
                    errorMessage = "";
                    return (IList<Subject>)subjects;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public IEnumerable<Subject>? GetAllSubjects(int courseId, out string errorMessage)
        {
            try
            {
                IEnumerable<Subject> subjects = _dbcontext.Subjects.Where(s => s.CourseId == courseId).ToList();
                
                if (subjects.Count() == 0)
                {
                    errorMessage = "No subjects found";
                    return null;
                }
                else
                {
                    errorMessage = "";
                    return subjects;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public Subject? UpdateSubject(int subjectId, int courseId, string subjectName, out string errorMessage)
        {
            try
            {
                Subject subject = _dbcontext.Subjects.FirstOrDefault(s => s.SubjectId == subjectId);

                if (subject != null)
                {
                    subject.CourseId = courseId;
                    subject.SubjectName = subjectName;
                    subject.DateModified = DateTime.Now;

                    _dbcontext.SaveChanges();

                    errorMessage = "";
                    return subject;
                }
                else
                {
                    errorMessage = "Subject not found";
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public Subject? GetSubject(int id, out string errorMessage)
        {
            try
            {
                Subject subject = _dbcontext.Subjects.FirstOrDefault(s => s.SubjectId == id);
                if (subject != null)
                {
                    errorMessage = "";
                    return subject;
                }
                else
                {
                    errorMessage = "Subject not found";
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public Subject? DeleteSubject(int subjectId, out string errorMessage, out bool isSuccess)
        {
            try
            {
                Subject subject = _dbcontext.Subjects.FirstOrDefault(s => s.SubjectId == subjectId);
                
                if (subject != null)
                {
                    if (subject.DateDeleted != null)
                    {
                        errorMessage = "Subject already deleted";
                        isSuccess = false;
                        return null;
                    }
                    else
                    {
                        subject.DateDeleted = DateTime.Now;
                        _dbcontext.SaveChanges();
                        errorMessage = "";
                        isSuccess = true;
                        return subject;
                    }
                }
                else
                {
                    errorMessage = "Subject not found";
                    isSuccess = false;
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                isSuccess = false;
                errorMessage = ex.Message;
                return null;
            }
        }
    }
}
