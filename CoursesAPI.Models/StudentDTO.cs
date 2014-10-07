using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
    public class StudentDTO
    {
        /// <summary>
        /// Auto generated ID
        /// </summary>
        public int          ID      { get; set; }

        /// <summary>
        /// SSN of the student
        /// </summary>
        [Required(ErrorMessage="SSN of a student is required!")]
        public string       SSN     { get; set; }

        /// <summary>
        /// Name of the student
        /// </summary>
        [Required(ErrorMessage="Name of a student is required!")]
        public string       Name    { get; set; }

        /// <summary>
        /// Email of the student
        /// </summary>
        [Required(ErrorMessage="Email of a student is required!")]
        public string       Email   { get; set; }

    }
}
