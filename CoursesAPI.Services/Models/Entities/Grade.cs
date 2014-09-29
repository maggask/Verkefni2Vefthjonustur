using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Services.Models.Entities
{
    class Grade
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
        public float Grade { get; set; }

        /// <summary>
        /// A reference to the Person table, SSN.
        /// </summary>
        public String PersonID { get; set; }
    }
}
