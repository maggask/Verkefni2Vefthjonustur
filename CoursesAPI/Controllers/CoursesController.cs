using System.Collections.Generic;
using System.Web.Http;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Services;
using System.Security.Claims;

namespace CoursesAPI.Controllers
{
    /// <summary>
    /// CoursesController represents recourses belonging to courses.
    /// </summary>   
	[RoutePrefix("api/courses")]
	public class CoursesController : ApiController
	{
		private readonly CoursesServiceProvider _service;

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
		public CoursesController()
		{ 
			_service = new CoursesServiceProvider(new UnitOfWork<AppDataContext>());
		}

        /// <summary>
        /// Returns a list of teachers in courses given a certain semester.
        /// </summary>
        /// <param name="semester"></param>
        /// <returns>Returns a list of teachers.</returns>
		[Authorize(Roles="student")]
        [Route("{semester}/teachers")]
		public List<Person> GetCourseTeachers(string semester)
		{
			return _service.GetCourseTeachers(semester);
		}
		
        /// <summary>
        /// Returns a list of courses taught on a certain semester
        /// </summary>
        /// <param name="semester"></param>
        /// <returns>Returns a list of courses.</returns>
		[Route("semester/{semester}")]
		public List<CourseInstanceDTO> GetCoursesOnSemester(string semester)
		{
			return _service.GetSemesterCourses(semester);
		}

        /// <summary>
        /// Returns a list of teachers that are teaching courses on a given semester.
        /// </summary>
        /// <param name="semester"></param>
        /// <returns>Returns a list of main teachers.</returns>
        [Route("semester/{semester}/teacher")]
        public List<CourseInstanceDTO> GetTeacherInCourse(string semester)
        {
            return _service.GetTeacherInCourse(semester);
        }

	}
}
