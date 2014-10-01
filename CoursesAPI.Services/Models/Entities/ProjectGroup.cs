using System;

namespace CoursesAPI.Services.Models.Entities
{
    public class ProjectGroup
    {
        /// <summary>
        /// A database-generated ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the project, e.g Verkefni, Lokapróf
        /// Netpróf, Endurtektarpróf og Miðannarpróf.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// How many e.g projects count to final
        /// grade in a course.
        /// </summary>
        public int GradedProjectsCount { get; set; }
    }
}
