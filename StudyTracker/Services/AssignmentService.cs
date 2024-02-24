using StudyTracker.Data;
using StudyTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace StudyTracker.Services
{
    public class AssignmentService
    {
        private readonly StudyTrackerDbContext _dbcontext;

        public AssignmentService(StudyTrackerDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public Assignment? AddAssignment(Assignment assignment, out string errorMessage)
        {
            try
            {
                assignment.DateAdded = DateTime.Now;
                _dbcontext.Assignments.Add(assignment);
                _dbcontext.SaveChanges();
                errorMessage = ""; // No error message as operation succeeded
                return assignment;
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message; // Set error message to exception messageW
                return null;
            }
        }

        public Assignment? GetAssignmentById(int assignmentId, out string errorMessage)
        {
            try
            {
                Assignment assignment = _dbcontext.Assignments
                    .Include(a => a.Course)
                    .Include(a => a.Subject)
                    .FirstOrDefault(a => a.AssignmentId == assignmentId);

                errorMessage = "";
                if (assignment == null)
                {
                    errorMessage = "Assignment not found";
                    return null;
                }
                else
                {
                    errorMessage = "";
                    return assignment;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public IEnumerable<Assignment>? GetAllAssignmentsBySubjectId(int subjectId, out string errorMessage)
        {
            try
            {
                var subject = _dbcontext.Subjects.FirstOrDefault(s => s.SubjectId == subjectId);
                if (subject == null)
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

            try
            {
                IEnumerable<Assignment> assignments = _dbcontext.Assignments.Where(a => a.SubjectId == subjectId).ToList();
                
                if (assignments.Count() == 0)
                {
                    errorMessage = "No assignments found";
                    return null;
                }
                else
                {
                    errorMessage = "";
                    return assignments;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }


        public IList<Assignment>? GetAllAssignmentsByUserId(string userId, out string errorMessage)
        {
            try
            {
                IList<Assignment> assignments = _dbcontext.Assignments
                    .Include(a => a.Course)
                    .Include(a => a.Subject)
                    .Where(a => a.AppUserID == userId && a.DateDeleted == null)
                    .ToList();

                if (assignments.Count() == 0)
                {
                    errorMessage = "No assignments found";
                    return null;
                }
                else
                {
                    errorMessage = "";
                    return assignments;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }


        // Update an assignment
        public Assignment? UpdateAssignment(Assignment assignment, out string errorMessage)
        {
            try
            {
                Assignment assignmentToUpdate = _dbcontext.Assignments.FirstOrDefault(a => a.AssignmentId == assignment.AssignmentId);

                if (assignmentToUpdate != null)
                {
                    assignmentToUpdate.CourseId = assignment.CourseId;
                    assignmentToUpdate.SubjectId = assignment.SubjectId;
                    assignmentToUpdate.Title = assignment.Title;
                    assignmentToUpdate.Description = assignment.Description;
                    assignmentToUpdate.Priority = assignment.Priority;
                    assignmentToUpdate.Status = assignment.Status;
                    assignmentToUpdate.DueDate = assignment.DueDate;
                    assignmentToUpdate.DateModified = DateTime.Now;
                    assignmentToUpdate.CompletionDate = assignment.CompletionDate;

                    _dbcontext.SaveChanges();

                    errorMessage = "";
                    return assignmentToUpdate;
                }
                else
                {
                    errorMessage = "Assignment not found";
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }   

        // Delete an assignment
        public Assignment? DeleteAssignment(int assignmentId, out string errorMessage, out bool isSuccess)
        {
            try
            {
                Assignment assignment = _dbcontext.Assignments.FirstOrDefault(a => a.AssignmentId == assignmentId);

                if (assignment != null)
                {
                    if (assignment.Status == Status.Completed)
                    {
                        errorMessage = "Assignment is completed and cannot be deleted";
                        isSuccess = false;
                        return assignment;
                    }
                    else if (assignment.DateDeleted != null)
                    {
                        errorMessage = "Assignment already deleted";
                        isSuccess = false;
                        return assignment;
                    }
                    else
                    {
                        assignment.DateDeleted = DateTime.Now;
                        _dbcontext.SaveChanges();
                        errorMessage = "";
                        isSuccess = true;
                        return assignment;
                    }
                }
                else
                {
                    errorMessage = "Assignment not found";
                    isSuccess = false;
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = ex.Message;
                isSuccess = false;
                return null;
            }
        }
    }
}
