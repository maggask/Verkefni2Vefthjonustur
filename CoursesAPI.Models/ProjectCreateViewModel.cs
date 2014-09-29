using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class ProjectCreateViewModel
    {
        public String   Name                     { get; set; }
        public int      Weight                   { get; set; }
        public float    MinGradeToPassCourse     { get; set; }
        public int      CourseInstanceID         { get; set; }
        public int      OnlyHigherThanProjectID  { get; set; }
    }
}
