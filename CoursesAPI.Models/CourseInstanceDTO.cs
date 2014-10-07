using System.ComponentModel.DataAnnotations;
namespace CoursesAPI.Models
{
	/// <summary>
	/// DTO class for an instance of a course.
	/// </summary>
	public class CourseInstanceDTO
	{
        /// <summary>
        /// Auto-generated id for a course instance.
        /// </summary>
		public int          CourseInstanceID { get; set; }

        /// <summary>
        /// ID of a course, foreign key.
        /// </summary>
        [Required(ErrorMessage="Course id is required!")]
		public string       CourseID         { get; set; }

        /// <summary>
        /// Name of the course.
        /// </summary>
        [Required(ErrorMessage="Name of a course is required!")]
		public string       Name             { get; set; }

        /// <summary>
        /// The general description of the course.
        /// </summary>
        [Required(ErrorMessage="Description of a course is required!")]
        public string       Description      { get; set; }

        /// <summary>
        /// The main teacher of the course,
        /// if it has one.
        /// </summary>
		public string       MainTeacher      { get; set; }
	}
}
