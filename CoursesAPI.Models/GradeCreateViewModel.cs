using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class GradeCreateViewModel
    {
        /// <summary>
        /// Instance ID of the project involved.
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// The students grade in a project involved.
        /// </summary>
        public float StudentGrade { get; set; }

        /// <summary>
        /// The students weighted grade(grade * weight/100) in a project involved.
        /// </summary>
        public float WeightedStudentGrade { get; set; }

        /// <summary>
        /// Instance ID of the person involved.
        /// </summary>
        public string PersonID { get; set; }
    }
}
