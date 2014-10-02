using System;

namespace CoursesAPI.Services.Models.Entities
{
    public class Grade
    {
        /// <summary>
        /// A database-generated ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// A reference to the Projects table.
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// A students grade.
        /// </summary>
        public float StudentGrade { get; set; }

        /// <summary>
        /// A students weighted grade(grade * weight/100).
        /// </summary>
        public float WeightedStudentGrade { get; set; }

        /// <summary>
        /// A reference to the Person table, SSN.
        /// </summary>
        public String PersonID { get; set; }
    }
}
