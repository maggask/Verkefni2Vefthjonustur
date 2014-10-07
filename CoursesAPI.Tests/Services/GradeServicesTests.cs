using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoursesAPI.Tests.MockObjects;
using CoursesAPI.Services.Services;
using CoursesAPI.Models;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Tests.Services
{
	/// <summary>
	/// Test class that runs test for
    /// GradeServiceProvider..
	/// </summary>
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
            _mockUow.SetRepositoryData(_mockFactory.GetMock<Person>());
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

        /// <summary>
        /// Test to get project group grade.
        /// </summary>
        [TestMethod]
        public void GradeTestGetProjectGroupGrade()
        {
            // Arrange:
            const String studentID = "1309862429";

            // Act:
            float groupGrade = _service.GetProjectGroupGrade(1, studentID);
 
            // Assert
            Assert.AreEqual(groupGrade,  (float)0.16);
        }

        /// <summary>
        /// Test to get project grade.
        /// </summary>
        [TestMethod]
        public void GradeTestGetGrade()
        {
            // Arrange:
            const String studentID = "1309862429";
            const int theGrade = 10; 

            //Act:
            var grade = _service.GetGrade(1, studentID);

            // Assert
            Assert.AreEqual(grade, theGrade);
        }

        /// <summary>
        /// Test to get final course grade.
        /// </summary>
        [TestMethod]
        public void GradeTestGetFinalGrade()
        {
            // Arrange:
            const float gradePassed1 = (float)4.0;
            const float gradePassed2 = (float)7.5;
            const float gradePassed3 = (float)0.0;
            const float gradePassed4 = (float)10.0;
            const String studentID1 = "1309862429";
            const String studentID2 = "2411903079";
            const String studentID3 = "1303922299";
            const String studentID4 = "0109753019";
             
            //Act:
            var finalGrade1 = _service.GetFinalGrade(1, studentID1);
            var finalGrade2 = _service.GetFinalGrade(1, studentID2);
            var finalGrade3 = _service.GetFinalGrade(1, studentID3);
            var finalGrade4 = _service.GetFinalGrade(1, studentID4);

            // Assert
            Assert.AreEqual(finalGrade1, gradePassed1);
            Assert.AreEqual(finalGrade2, gradePassed2);
            Assert.AreEqual(finalGrade3, gradePassed3);
            Assert.AreEqual(finalGrade4, gradePassed4); 
        }

        /// <summary>
        /// Test to get a stundents ranking
        /// in a given project.
        /// </summary>
        [TestMethod]
        public void GradeTestGetProjectRankings()
        {
            // Arrange:
            const String studentID1 = "1309862429";
            const String studentID2 = "1303922299";
            const String studentID3 = "2411903079";
          
            //Act:
            var projectRankingsFirst = _service.GetProjectRankings(studentID1, 1);
            var projectRankingsSecond = _service.GetProjectRankings(studentID2, 1);
            var projectRankingsEqual = _service.GetProjectRankings(studentID3, 1);
            
            // Assert            
            Assert.IsTrue(projectRankingsFirst == "1/4");
            Assert.IsTrue(projectRankingsSecond == "2/4");
            Assert.IsTrue(projectRankingsEqual == "3-4/4");
        }

        /// <summary>
        /// Test to get the final ranking of
        /// a student amongst peers.
        /// </summary>
        [TestMethod]
        public void GradeTestGetFinalRankings()
        {
            // Arrange:
            const String studentID1 = "0109753019";
            const String studentID2 = "1303922299";
            const String studentID3 = "2411903079";

            //Act:

            var finalRankingsFirst = _service.GetFinalRankings(1, studentID1);
            var finalRankingsLast = _service.GetFinalRankings(1, studentID2);
            var finalRankingsEqual = _service.GetFinalRankings(1, studentID3);

            // Assert
            Assert.IsTrue(finalRankingsFirst == "1/5");
            Assert.IsTrue(finalRankingsEqual == "2-3/5");
            Assert.IsTrue(finalRankingsLast == "5/5");
        }

	}     
}
