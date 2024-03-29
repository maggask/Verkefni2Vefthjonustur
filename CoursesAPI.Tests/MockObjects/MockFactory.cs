﻿using System;
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
            var gradesList = new List<Grade>();
            var projectList = new List<Project>();
            var projectGroupList = new List<ProjectGroup>();
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

            personList.Add(new Person
            {
                ID = 4,
                SSN = "0101862349",
                Name = "Þórður Þorsteinsson",
                Email = "thordurt12@ru.is"
            });

            personList.Add(new Person
            {
                ID = 5,
                SSN = "0109753019",
                Name = "Freyr Bergsteinsson",
                Email = "freyrb12@ru.is"
            });

            #endregion

            #region Mock data - Grades

            // Margrét
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
                StudentGrade = (float)6.666667,
                WeightedStudentGrade = (float)4,
                PersonID = "1309862429",
            });

            gradesList.Add(new Grade
            {
                ID = 4,
                ProjectID = 2,
                StudentGrade = 6,
                WeightedStudentGrade = (float)0.12,
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

            // Anna Laufey
            gradesList.Add(new Grade
            {
                ID = 6,
                ProjectID = 1,
                StudentGrade = 6,
                WeightedStudentGrade = (float)0.12,
                PersonID = "1303922299",
            });

            gradesList.Add(new Grade
            {
                ID = 7,
                ProjectID = 4,
                StudentGrade = 0,
                WeightedStudentGrade = (float)0.0,
                PersonID = "1303922299",
            });

            gradesList.Add(new Grade
            {
                ID = 8,
                ProjectID = 6,
                StudentGrade = 0,
                WeightedStudentGrade = (float)0,
                PersonID = "1303922299",
            });

            gradesList.Add(new Grade
            {
                ID = 9,
                ProjectID = 2,
                StudentGrade = 5,
                WeightedStudentGrade = (float)0.1,
                PersonID = "1303922299",
            });

            gradesList.Add(new Grade
            {
                ID = 10,
                ProjectID = 5,
                StudentGrade = 3,
                WeightedStudentGrade = (float)0.93,
                PersonID = "1303922299",
            });

            gradesList.Add(new Grade
            {
                ID = 12,
                ProjectID = 5,
                StudentGrade = 5,
                WeightedStudentGrade = (float)1.5,
                PersonID = "0101862349",
            });
            gradesList.Add(new Grade
            {
                ID = 11,
                ProjectID = 7,
                StudentGrade = 10,
                WeightedStudentGrade = (float)3,
                PersonID = "0101862349",
            });

            gradesList.Add(new Grade
            {
                ID = 12,
                ProjectID = 1,
                StudentGrade = 0,
                WeightedStudentGrade = (float)0,
                PersonID = "0101862349",
            });

            gradesList.Add(new Grade
            {
                ID = 12,
                ProjectID = 6,
                StudentGrade = 10,
                WeightedStudentGrade = (float)6,
                PersonID = "0101862349",
            });

            gradesList.Add(new Grade
            {
                ID = 13,
                ProjectID = 5,
                StudentGrade = 5,
                WeightedStudentGrade = (float)1.5,
                PersonID = "2411903079",
            });

            gradesList.Add(new Grade
            {
                ID = 14,
                ProjectID = 7,
                StudentGrade = 10,
                WeightedStudentGrade = (float)3.0,
                PersonID = "2411903079",
            });

            gradesList.Add(new Grade
            {
                ID = 15,
                ProjectID = 1,
                StudentGrade = 0,
                WeightedStudentGrade = (float)0.0,
                PersonID = "2411903079",
            });

            gradesList.Add(new Grade
            {
                ID = 16,
                ProjectID = 6,
                StudentGrade = 10,
                WeightedStudentGrade = (float)6,
                PersonID = "2411903079",
            });

            gradesList.Add(new Grade
            {
                ID = 17,
                ProjectID = 6,
                StudentGrade = 50,
                WeightedStudentGrade = (float)30,
                PersonID = "0109753019",
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
                Weight = 6,
                MinGradeToPassCourse = null,
            });

            projectList.Add(new Project
            {
                ID = 5,
                Name = "Skilaverkefni2",
                ProjectGroupID = 2,
                CourseInstanceID = 1,
                OnlyHigherThanProjectID = null,
                Weight = 30,
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

            projectList.Add(new Project
            {
                ID = 7,
                Name = "Fyrirlestur",
                ProjectGroupID = null,
                CourseInstanceID = 1,
                OnlyHigherThanProjectID = 5,
                Weight = 30,
                MinGradeToPassCourse = null,
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

            _repositories.Add(typeof(Person), personList);
            _repositories.Add(typeof(Grade), gradesList);
            _repositories.Add(typeof(Project), projectList);
            _repositories.Add(typeof(ProjectGroup), projectGroupList);
            #endregion
        }

        /// <summary>
        /// Allows the data from MockFactory to be accessible
        /// in GradeServiceTests.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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
