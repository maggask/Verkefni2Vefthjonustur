using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class ProjectGroupCreateViewModel
    {
        /// <summary>
        /// A database-generated ID
        /// </summary>
        public int          ID                      { get; set; }

        /// <summary>
        /// The name of the project, e.g Verkefni, Lokapróf
        /// Netpróf, Endurtektarpróf og Miðannarpróf.
        /// </summary>
        [Required(ErrorMessage="Name required")]
        public String       Name                    { get; set; }

        /// <summary>
        /// How many e.g projects count to final
        /// grade in a course.
        /// </summary>
        [Required(ErrorMessage="How many projects count towards grade required")]
        public int          GradedProjectsCount     { get; set; }
    }
}
