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
        /// <returns>Returns the final grade in a course or current grade.</returns>
        [HttpGet]
        [Route("GetFinalGrade/{studentID}")]
        public String GetFinalGrade(int courseInstanceID, String studentID)
        {
            return _service.GetFinalGrade(courseInstanceID, studentID);
        }

        /// <summary>
        /// Gets a students ranking in a
        /// single project.
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="projectID"></param>
        /// <returns></returns>
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
        /// <param name="projectID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFinalRankings/{studentID}")]
        public String GetFinalRankings(int courseInstanceID, String studentID)
        {
            return _service.GetFinalRankings(courseInstanceID, studentID);
        }

        /// <summary>
        /// Returns all grades for a single student
        /// </summary>
        /// <param name="studentID"></param>
        /// <returns></returns>
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
        /// <returns></returns>
        [HttpGet]
        [Route("ProjectOverView/{projectID}")]
        public List<String> ProjectOverView(int projectID)
        {
            return _service. ProjectOverView(projectID);
        }

        /// <summary>
        /// Returns a list with all student names and there grade in a current course
        /// </summary>
        /// <param name="courseInstanceID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("FinalGradeOverView")]
        public List<String> FinalGradeOverView(int courseInstanceID)
        {
            return _service.FinalGradeOverView(courseInstanceID);
        }
    }
}
