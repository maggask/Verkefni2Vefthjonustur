using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage="Name required")]
        public String   Name                     { get; set; }

        /// <summary>
        /// Weight of the project, 5 for 5%, 10 for 10% ect.
        /// </summary>
        [Required(ErrorMessage="Project weight required")]
        [Range(0, 100)]
        public int      Weight                   { get; set; }

        /// <summary>
        /// The minimal grade to pass the course.
        /// </summary>
        [Range(0, 10)]
        public float?   MinGradeToPassCourse     { get; set; }

        /// <summary>
        /// If a project is set to push up a given grade
        /// if you score higher on that project than
        /// something else, this ID is used. Can be empty.
        /// </summary>
        public int?     OnlyHigherThanProjectID  { get; set; }

        /// <summary>
        /// The group id that the project belongs to, if any.
        /// </summary>
        public int?     ProjectGroupID           { get; set; }
    }
}
