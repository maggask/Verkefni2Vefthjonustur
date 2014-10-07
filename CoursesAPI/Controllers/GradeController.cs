using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoursesAPI.Services;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Services;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Models;

namespace CoursesAPI.Controllers
{
    /// <summary>
    /// GradeController represents resources belonging to grades.
    /// </summary>
    [Authorize]
    [RoutePrefix("api/courses/{courseInstanceID}")]
    public class GradeController : ApiController
    {
        private readonly GradeServiceProvider _service;

        /// <summary>
        /// Constructor for the controller.
        /// </summary>
        public GradeController()
        {
            _service = new GradeServiceProvider(new UnitOfWork<AppDataContext>());
        }

        /// <summary>
        /// Adds new projects of different types
        /// to a given course instance.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns the project that was created.</returns>
        [Authorize(Roles="teacher")]
        [HttpPost]
        [Route("PostProject")]
        public Project PostProject(ProjectCreateViewModel model)
        {
            return _service.AddProject(model);
        }

        /// <summary>
        /// Adds a new grade to projects of
        /// different types in a given
        /// course instance.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns the grade that was given.</returns>
        [Authorize(Roles="teacher")]
        [HttpPost]
        [Route("PostGrade")]
        public Grade PostGrade(GradeCreateViewModel model)
        {
            return _service.AddGrade(model);
        }

        /// <summary>
        /// Adds a new project group of 
        /// different types in a given
        /// course instance.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Returns the group that was created.</returns>
        [Authorize(Roles="teacher")]
        [HttpPost]
        [Route("PostProjectGroup")]
        public ProjectGroup PostProjectGroup(ProjectGroupCreateViewModel model)
        {
            return _service.AddProjectGroup(model);
        }

        /// <summary>
        /// Gets a student grade from
        /// specific project.
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="studentID"></param>
        /// <returns>Return grade for project.</returns>
        [Authorize(Roles="student,teacher")]
        [HttpGet]
        [Route("GetGrade/{projectID}/{studentID}")]
        public float GetGrade(int projectID, String studentID)
        {
            return _service.GetGrade(projectID, studentID);
        }

        /// <summary>
        /// Gets the final grade of a given
        /// student or the current grade.
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="courseInstanceID"></param>
        /// <returns>Returns the final grade in a course or current grade.</returns>
        [Authorize(Roles="student,teacher")]
        [HttpGet]
        [Route("GetFinalGrade/{studentID}")]
        public float GetFinalGrade(int courseInstanceID, String studentID)
        {
            return _service.GetFinalGrade(courseInstanceID, studentID);
        }

        /// <summary>
        /// Gets a students ranking in a
        /// single project.
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="projectID"></param>
        /// <returns>Returns the ranking of a given student in a course.</returns>
        [Authorize(Roles="student,teacher")]
        [HttpGet]
        [Route("GetProjectRankings/{studentID}/{projectID}")]
        public String GetProjectRankings(String studentID, int projectID)
        {
            return _service.GetProjectRankings(studentID, projectID);
        }

        /// <summary>
        /// Gets a students final grade ranking
        /// among others in a given course.
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="courseInstanceID"></param>
        /// <returns>Returns a students final rankings in a course.</returns>
        [Authorize(Roles="student,teacher")]
        [HttpGet]
        [Route("GetFinalRankings/{studentID}")]
        public String GetFinalRankings(int courseInstanceID, String studentID)
        {
            return _service.GetFinalRankings(courseInstanceID, studentID);
        }

        /// <summary>
        /// Returns all grades for a single student.
        /// </summary>
        /// <param name="studentID"></param>
        /// <returns>Returns all grades of a given student.</returns>
        [Authorize(Roles="student,teacher")]
        [HttpGet]
        [Route("GetAllGradesByStudent/{studentID}")]
        public List<float> GetAllGradesByStudent(String studentID)
        {
            return _service.GetAllGradesByStudent(studentID);
        }

        /// <summary>
        /// Returns a list of all students and there grade for the given project
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns>Returns a list of students projects and there grades.</returns>
        [Authorize(Roles="teacher")]
        [HttpGet]
        [Route("ProjectOverView/{projectID}")]
        public List<String> GetProjectOverView(int projectID)
        {
            return _service.GetProjectOverView(projectID);
        }

        /// <summary>
        /// Returns a list with all student names and there grade in a current course
        /// </summary>
        /// <param name="courseInstanceID"></param>
        /// <returns>Returns a list of students final grades.</returns>
        [HttpGet]
        [Authorize(Roles="teacher")]
        [Route("FinalGradeOverView")]
        public List<String> GetFinalGradeOverView(int courseInstanceID)
        {
            return _service.GetFinalGradeOverView(courseInstanceID);
        }
    }
}
