using System;

namespace CoursesAPI.Services.Models.Entities
{
    public class Project
    {
        /// <summary>
        /// A database-generate ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of the project, e.g skilaverkefni1, lokapróf,
        /// netpróf1, miðannarpróf.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// A reference to the CourseInstance table. The
        /// instance of a course that e.g the project
        /// applies to.
        /// </summary>
        public int CourseInstanceID { get; set; }

        /// <summary>
        /// Some "projects" like midtermtest (miðannarpróf) 
        /// can help push up the final exam (lokapróf) grade if e.g 
        /// a student scores higher on the midtermtest than the final exam.
        /// This can also be other projects. Allows null.
        /// </summary>
        public int? OnlyHigherThanProjectID { get; set; }

        /// <summary>
        /// The weigth of a project, test et cetera, 5 is 5%,
        /// 10 is 10% and so on.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// The minimal grade to be able to pass
        /// the course. That is e.g final grade
        /// of the final exam and the final grade
        /// in the course.
        /// </summary>
        public float? MinGradeToPassCourse { get; set; }

        /// <summary>
        /// The group id that the project belongs to, if any.
        /// </summary>
        public int? ProjectGroupID { get; set; }
    }
}
