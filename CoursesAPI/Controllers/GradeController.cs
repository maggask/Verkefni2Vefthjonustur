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
        [Route("{action}")]
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
        [Route("{action}")]
        public  Grade PostGrade(GradeCreateViewModel model)
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
        [Route("{action}")]
        public ProjectGroup PostProjectGroup(ProjectGroupCreateViewModel model)
        {
            return _service.AddProjectGroup(model);
        }
    }
}
