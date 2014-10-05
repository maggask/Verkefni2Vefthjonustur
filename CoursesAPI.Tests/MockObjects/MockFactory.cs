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

            #region Grade service provider test
            #region List instances
            var personList = new List<Person>();
            var courseInstanceList = new List<CourseInstance>();
            var courseTemplateList = new List<CourseTemplate>();
            var gradesList = new List<Grade>();
            var projectList = new List<Project>();
            var projectGroupList = new List<ProjectGroup>();
            var semesterList = new List<Semester>();
            #endregion

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

            #region Mock data - Grades

            gradesList.Add(new Grade
            {
                ID = 1, 
                ProjectID = 1, 
                StudentGrade = 10,
                WeightedStudentGrade = (float)0.2,
                PersonID = "1309862429",

            });

            gradesList.Add(new Grade
            {
                ID = 2,
                ProjectID = 4,
                StudentGrade = 10,
                WeightedStudentGrade = (float)0.5,
                PersonID = "1309862429",

            });
            gradesList.Add(new Grade
            {
                ID = 3,
                ProjectID = 6,
                StudentGrade = 10,
                WeightedStudentGrade = (float)6,
                PersonID = "1309862429",

            });

            gradesList.Add(new Grade
            {
                ID = 4,
                ProjectID = 2,
                StudentGrade = 6,
                WeightedStudentGrade =(float)0.12,
                PersonID = "1309862429",

            });

            gradesList.Add(new Grade
            {
                ID = 5,
                ProjectID = 5,
                StudentGrade = 3,
                WeightedStudentGrade = (float)0.93,
                PersonID = "1309862429",

            });
        
            //hallo

            gradesList.Add(new Grade
            {
                ID = 1,
                ProjectID = 1,
                StudentGrade = 6,
                WeightedStudentGrade = (float)0.12,
                PersonID = "1303922299",

            });

            gradesList.Add(new Grade
            {
                ID = 2,
                ProjectID = 4,
                StudentGrade = 5,
                WeightedStudentGrade = (float)0.25,
                PersonID = "1303922299",

            });
            gradesList.Add(new Grade
            {
                ID = 3,
                ProjectID = 6,
                StudentGrade = 5,
                WeightedStudentGrade = (float)3,
                PersonID = "1303922299",

            });

            gradesList.Add(new Grade
            {
                ID = 4,
                ProjectID = 2,
                StudentGrade = 5,
                WeightedStudentGrade = (float)0.1,
                PersonID = "1303922299",

            });

            gradesList.Add(new Grade
            {
                ID = 5,
                ProjectID = 5,
                StudentGrade = 3,
                WeightedStudentGrade = (float)0.93,
                PersonID = "1303922299",

            });
        

            #endregion

            #region Mock data - Projects

            projectList.Add(new Project
            {
                ID = 1, 
                Name = "Netpróf1",
                ProjectGroupID = 1, 
                CourseInstanceID = 1, 
                OnlyHigherThanProjectID = null, 
                Weight = 2, 
                MinGradeToPassCourse = null,

            });

            projectList.Add(new Project
            {
                ID = 2,
                Name = "Netpróf2",
                ProjectGroupID = 1,
                CourseInstanceID = 1,
                OnlyHigherThanProjectID = null,
                Weight = 2,
                MinGradeToPassCourse = null,

            });

            projectList.Add(new Project
            {
                ID = 3,
                Name = "Netpróf3",
                ProjectGroupID = 1,
                CourseInstanceID = 1,
                OnlyHigherThanProjectID = null,
                Weight = 2,
                MinGradeToPassCourse = null,

            });

            projectList.Add(new Project
            {
                ID = 4,
                Name = "Skilaverkefni1",
                ProjectGroupID = 2,
                CourseInstanceID = 1,
                OnlyHigherThanProjectID = null,
                Weight = 5,
                MinGradeToPassCourse = null,

            });
            projectList.Add(new Project
            {
                ID = 5,
                Name = "Skilaverkefni2",
                ProjectGroupID = 2,
                CourseInstanceID = 1,
                OnlyHigherThanProjectID = null,
                Weight = 31,
                MinGradeToPassCourse = null,

            });

            projectList.Add(new Project
            {
                ID = 6,
                Name = "Lokapróf",
                ProjectGroupID = null,
                CourseInstanceID = 1,
                OnlyHigherThanProjectID = null,
                Weight = 60,
                MinGradeToPassCourse = 5,

            });

            
            #endregion

            #region Mock data - ProjectGroup
            projectGroupList.Add(new ProjectGroup
            {
                ID = 1,
                Name = "Netpróf",
                GradedProjectsCount = 2,

            });

            projectGroupList.Add(new ProjectGroup
            {
                ID = 2,
                Name = "Skilaverkefni",
                GradedProjectsCount = 4,

            });
            #endregion

            #region Mock data - Semester



            #endregion


            _repositories.Add(typeof(Person), personList);
            _repositories.Add(typeof(CourseInstance), courseInstanceList);
            _repositories.Add(typeof(CourseTemplate), courseTemplateList);
            _repositories.Add(typeof(Grade), gradesList);
            _repositories.Add(typeof(Project), projectList);
            _repositories.Add(typeof(ProjectGroup), projectGroupList);
            _repositories.Add(typeof(Semester), semesterList);
            #endregion
        }
        public List<T> GetMock<T>() where T : class
        {
            if (_repositories.Keys.Contains(typeof(T)))
            {
                return _repositories[typeof(T)] as List<T>;
            }

            return null;
        }
    }
}
