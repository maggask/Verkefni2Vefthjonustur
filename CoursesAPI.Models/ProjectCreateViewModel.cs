using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class ProjectCreateViewModel
    {
        /// <summary>
        /// The project name, e.g lokapróf, skilaverkefni...
        /// </summary>
        public String   Name                     { get; set; }

        /// <summary>
        /// Weight of the project, 5 for 5%, 10 for 10% ect.
        /// </summary>
        public int      Weight                   { get; set; }

        /// <summary>
        /// The minimal grade to pass the course.
        /// </summary>
        public float    MinGradeToPassCourse     { get; set; }

        /// <summary>
        /// The instance ID of the given course.
        /// </summary>
        public int      CourseInstanceID         { get; set; }

        /// <summary>
        /// If a project is set to push up a given grade
        /// if you score higher on that project than
        /// something else, this ID is used. Can be empty.
        /// </summary>
        public int?     OnlyHigherThanProjectID  { get; set; }
    }
}
