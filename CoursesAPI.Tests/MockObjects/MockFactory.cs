using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Tests.MockObjects
{
    /// <summary>
    /// Class that returns mock data
    /// for unit testing.
    /// </summary>
    public class MockFactory
    {
        private readonly Dictionary<Type, object> _repositories;

        /// <summary>
        /// Constructor for mock factory.
        /// </summary>
        public MockFactory()
        {
            _repositories = new Dictionary<Type, object>();

            var personList = new List<Person>();

            #region Mock data - Persons
            personList.Add(new Person
            {
                ID = 1,
                SSN = "1309862429",
                Name = "Margrét S. Kristjánsdóttir",
                Email = "margretk12@ru.is"
            });

            personList.Add(new Person
            {
                ID = 2,
                SSN = "2411903079",
                Name = "Einar Þór Traustason",
                Email = "einart13@ru.is"
            });

            personList.Add(new Person
            {
                ID = 3,
                SSN = "1303922299",
                Name = "Anna Laufey Stefánsdóttir",
                Email = "annals12@ru.is"
            });
            #endregion

            _repositories.Add(typeof(Person), personList);

            #region Grade service provider test
            #region List instances
            var courseInstanceList = new List<CourseInstance>();
            var courseTemplateList = new List<CourseTemplate>();
            var gradesList = new List<Grade>();
            var projectList = new List<Project>();
            var projectGroupList = new List<ProjectGroup>();
            var semesterList = new List<Semester>();
            #endregion

            #region Mock data - Grades

            #endregion

            _repositories.Add(typeof(CourseInstance), courseInstanceList);
            _repositories.Add(typeof(CourseTemplate), courseTemplateList);
            _repositories.Add(typeof(Grade), gradesList);
            _repositories.Add(typeof(Project), projectList);
            _repositories.Add(typeof(ProjectGroup), projectGroupList);
            _repositories.Add(typeof(Semester), semesterList);
            #endregion
        }
    }
}
