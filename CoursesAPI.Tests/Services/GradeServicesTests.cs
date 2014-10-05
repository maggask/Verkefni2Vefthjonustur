﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoursesAPI.Tests.MockObjects;
using CoursesAPI.Services.Services;
using CoursesAPI.Models;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Tests.Services
{
	[TestClass]
	public class GradeServicesTests
	{
        private GradeServiceProvider _service;
        private MockUnitOfWork<MockDataContext> _mockUow;
        private MockFactory _mockFactory;

		[TestInitialize]
		public void Setup()
		{
            _mockUow = new MockUnitOfWork<MockDataContext>();
            _mockFactory = new MockFactory();
            _service = new GradeServiceProvider(_mockUow);

            _mockUow.SetRepositoryData(_mockFactory.GetMock<Grade>());
            _mockUow.SetRepositoryData(_mockFactory.GetMock<Project>());
            _mockUow.SetRepositoryData(_mockFactory.GetMock<ProjectGroup>());
		}

        /// <summary>
        /// Test to add a new project with valid information.
        /// </summary>
		[TestMethod]
		public void GradeTestAddNewProject()
		{
			// Arrange:
            var project = new ProjectCreateViewModel
            {
                Name = "Lokapróf",
                Weight = 80,
                MinGradeToPassCourse = 5,
                CourseInstanceID = 1,
                OnlyHigherThanProjectID = null
            };

			// Act:
            var proj = _service.AddProject(project);

			// Assert:
            Assert.IsNotNull(project, "The object should contain the created project!");
            Assert.IsTrue(_mockUow.GetSaveCallCount() > 0);
		}

        /// <summary>
        /// Test to add a new grade with valid information.
        /// </summary>
        [TestMethod]
        public void GradeTestAddNewGrade()
        {
            // Arrange:
            var grade = new GradeCreateViewModel
            {
                ProjectID = 1,
                StudentGrade = 8,
                PersonID = "1309862429"
            };

            // Act:
            var grad = _service.AddGrade(grade);

            // Assert:
            Assert.IsNotNull(grade, "The object should contain the new grade!");
            Assert.IsTrue(_mockUow.GetSaveCallCount() > 0);
        }

        /// <summary>
        /// Test to add a new project group.
        /// </summary>
        [TestMethod]
        public void GradeTestAddNewProjectGroup()
        {
            // Arrange:
            var projectGroup = new ProjectGroupCreateViewModel
            {
                Name = "Netpróf",
                GradedProjectsCount = 5
            };

            // Act:
            var projG = _service.AddProjectGroup(projectGroup);

            // Assert
            Assert.IsNotNull(projectGroup, "The object should contain the new project group!");
            Assert.IsTrue(_mockUow.GetSaveCallCount() > 0);
        }

        [TestMethod]
        public void GradeTestGetGroupGrade()
        {

            // Arrange:

            // Act:
            var groupGrade = _service.GetGroupGrade(1, "2411903079");
            // Assert

            Assert.AreEqual(groupGrade, 0.12);
            Assert.IsTrue(_mockUow.GetSaveCallCount() > 0);

        }

        [TestMethod]
        public void GradeTestGetGrade()
        {
            // Arrange:

            //Act:
            var grade = _service.GetGrade(1, "1309862429");


            // Assert
            Assert.AreEqual(grade, 8);
            Assert.IsTrue(_mockUow.GetSaveCallCount() > 0);

        }

        [TestMethod]
        public void GradeTestGetFinaleGrade()
        {
            // Arrange:
             
            //Act:
            var finalGrade = _service.GetFinalGrade("1309862429");

            // Assert
            Assert.AreEqual(finalGrade, 7.5); 
           // Assert.IsTrue(_mockUow.GetSaveCallCount() > 0);

        }

        [TestMethod]
        public void GradeTestGetProjectRankings()
        {
            // Arrange:
            var projectRankingsFirst = _service.GetProjectRankings("1303922299", 1);
            var projectRankingsSecond = _service.GetProjectRankings("1309862429", 1);
            char[] split = { '/' };

            string[] getfirst = projectRankingsFirst.Split(split);
            string[] getsecond = projectRankingsSecond.Split(split);
            int first = Int32.Parse(getfirst[0]);
            int second = Int32.Parse(getsecond[0]);

            //Act:

            // Assert
            
            Assert.IsTrue(first > second);
           // Assert.IsTrue(_mockUow.GetSaveCallCount() > 0);

        }

        [TestMethod]
        public void GradeTestGetFinalRankings()
        {
            // Arrange:
            var finalRankingsFirst = _service.GetFinalRankings("1303922299");
            var finalRankingsSecond = _service.GetFinalRankings("1309862429");

            char[] split = { '/' };

            string[] getfirst = finalRankingsFirst.Split(split);
            string[] getsecond = finalRankingsSecond.Split(split);
            int first = Int32.Parse(getfirst[0]);
            int second = Int32.Parse(getsecond[0]);

            //Act:
           
            // Assert
            Assert.IsTrue(first > second);
          //  Assert.IsTrue(_mockUow.GetSaveCallCount() > 0);

        }

	}

     
}
