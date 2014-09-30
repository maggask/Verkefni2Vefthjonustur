using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class GradeCreateViewModel
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public float StudentGrade { get; set; }
        public string PersonID { get; set; }
    }
}
