using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursesAPI.Services.Helpers;

namespace CoursesAPI.Services.Services
{
    public class GradeServiceProvider
    {
        private readonly IUnitOfWork _uow;

		private readonly IRepository<CourseInstance> _courseInstances;
		private readonly IRepository<CourseTemplate> _courseTemplates;
        private readonly IRepository<Grade> _grades;
		private readonly IRepository<Person> _persons;
        private readonly IRepository<Project> _projects;
        private readonly IRepository<ProjectGroup> _projectGroups;
        private readonly IRepository<Semester> _semesters;

		public GradeServiceProvider(IUnitOfWork uow)
		{
			_uow = uow;

			_courseInstances      = _uow.GetRepository<CourseInstance>();
			_courseTemplates      = _uow.GetRepository<CourseTemplate>();
            _grades               = _uow.GetRepository<Grade>();
			_persons              = _uow.GetRepository<Person>();
            _projects             = _uow.GetRepository<Project>();
            _projectGroups        = _uow.GetRepository<ProjectGroup>();
            _semesters            = _uow.GetRepository<Semester>();
		}

        /// <summary>
        /// Adds a project to a current
        /// course instance.
        /// </summary>
        /// <param name="model"></param>
        public Project AddProject(ProjectCreateViewModel model)
        {
            CourseAPIValidation.Validate(model);

            var p = new Project
            {
                Name = model.Name,
                Weight = model.Weight,
                MinGradeToPassCourse = model.MinGradeToPassCourse,
                CourseInstanceID = model.CourseInstanceID,
                ProjectGroupID = model.ProjectGroupID,
                OnlyHigherThanProjectID = model.OnlyHigherThanProjectID
            };

            _projects.Add(p);
            _uow.Save();

            return p;
        }

        /// <summary>
        /// Adds grade for a student for
        /// a given project.
        /// </summary>
        /// <param name="model"></param>
        public Grade AddGrade(GradeCreateViewModel model)
        {
            CourseAPIValidation.Validate(model);

            var weight = (from w in _projects.All()
                          where w.ID == model.ProjectID
                          select w.Weight).SingleOrDefault();

            var g = new Grade
            {
                PersonID = model.PersonID,
                ProjectID = model.ProjectID,
                StudentGrade = model.StudentGrade,
                WeightedStudentGrade = (model.StudentGrade * weight/100)
            };

            _grades.Add(g);
            _uow.Save();

            return g;
        }

        /// <summary>
        /// Adds a project group
        /// for a given course instance.
        /// </summary>
        /// <param name="model"></param>
        public ProjectGroup AddProjectGroup(ProjectGroupCreateViewModel model)
        {
            CourseAPIValidation.Validate(model);

            var pg = new ProjectGroup
            {
                Name = model.Name,
                GradedProjectsCount = model.GradedProjectsCount
            };

            _projectGroups.Add(pg);
            _uow.Save();

            return pg;
        }
        
        /// <summary>
        /// Helper function that returns
        /// a total grade for a given
        /// group.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Returns the total grade as float.</returns>
        public float GetProjectGroupGrade(int ID, String studentID)
        {
            var theGroup = (from h in _projectGroups.All()
                           where (h.ID == ID)
                           select h).SingleOrDefault();

            int howMany = theGroup.GradedProjectsCount;

            var allProjects = (from g in _grades.All()
                               join p in _projects.All() on g.ProjectID equals p.ID 
                               orderby g.StudentGrade descending
                               where p.ProjectGroupID == ID
                                    && g.PersonID == studentID
                               select g).Take(howMany).ToList();

            var weight = (from p in _projects.All()
                          where p.ProjectGroupID == ID
                          select p.Weight).FirstOrDefault();

            return allProjects.Average(x => x.WeightedStudentGrade);
        }

        /// <summary>
        /// Gets current final grade of course
        /// for a given student.
        /// </summary>
        /// <param name="studentID"></param>
        /// <returns>Returns the final grade for a given student.</returns>
        public float GetCurrentFinalGrade(int courseInstanceID, String studentID)
        {
            float finalGrade = 0;

            var allBasicGrades = (from g in _grades.All()
                             join p in _projects.All() on g.ProjectID equals p.ID 
                             where (g.PersonID == studentID 
                                    && p.CourseInstanceID == courseInstanceID
                                    && p.ProjectGroupID == null
                                    && p.OnlyHigherThanProjectID == null)
                             select g).ToList();

            foreach(var grade in allBasicGrades)
            {
                finalGrade =+ (grade.WeightedStudentGrade);
            }

            var allGroupGrades = (from g in _grades.All()
                                  join p in _projects.All() on g.ProjectID equals p.ID
                                  where (g.PersonID == studentID
                                        && p.CourseInstanceID == courseInstanceID
                                        && p.ProjectGroupID != null
                                        && p.OnlyHigherThanProjectID == null)
                                  orderby (p.ProjectGroupID) descending
                                  select p).ToList();

            float previous = -1;

            foreach (var p in allGroupGrades)
            {
                if (p.ProjectGroupID == previous)
                {
                    continue;
                }

                finalGrade += GetProjectGroupGrade(p.ProjectGroupID.GetValueOrDefault(), studentID);
                previous = p.ProjectGroupID.GetValueOrDefault();
            }

            var allHigherThanProjects = (from g in _grades.All()
                                        join p in _projects.All() on g.ProjectID equals p.ID
                                        where (g.PersonID == studentID
                                                && p.CourseInstanceID == courseInstanceID
                                                && p.ProjectGroupID == null
                                                && p.OnlyHigherThanProjectID != null)
                                        select p).ToList();

            foreach (var proj in allHigherThanProjects)
            {
                var grade1 = (from g in _grades.All()
                              where g.ProjectID == proj.ID
                                    && g.PersonID == studentID
                              select g.WeightedStudentGrade).SingleOrDefault();

                var grade2 = (from g in _grades.All()
                              where g.ProjectID == proj.OnlyHigherThanProjectID
                                    && g.PersonID == studentID
                              select g.WeightedStudentGrade).SingleOrDefault();

                if (grade1 > grade2)
                {
                    finalGrade += (grade1 - grade2);
                }
            }

            return finalGrade;
        }

        /// <summary>
        /// Gets students grade for
        /// a given project.
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="studentID"></param>
        /// <returns>Returns the grade of a project has float.</returns>
        public float GetGrade(int projectID, String studentID)
        {
            var grade = (from g in _grades.All()
                         where g.ProjectID == projectID
                                && g.PersonID == studentID
                         select g.StudentGrade).SingleOrDefault();

            return grade;
        }

        /// <summary>
        /// Helper function that goes through all
        /// the grades and orders them descending.
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns>Returns the ordered list of grades.</returns>
        public List<float> AllGradesInOrder(int projectID)
        {
            var allGrades = (from g in _grades.All()
                             where g.ProjectID == projectID
                             orderby g.StudentGrade descending
                             select g.StudentGrade).ToList();

            return allGrades;
        }

        /// <summary>
        /// Gets a students project grade ranking
        /// among others in a given course. If you have the 
        /// same grade as others you wont get a precise ranking.
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="projectID"></param>
        /// <returns>Returns the students ranking as a string.</returns>
        public String GetProjectRankings(String studentID, int projectID)
        {
            var allGrades = AllGradesInOrder(projectID);
            var reverse = new List<float>(allGrades);
            reverse.Sort();
            
            var grade = GetGrade(projectID, studentID);

            int frontStanding = ((allGrades.IndexOf(grade)) + 1);
            int backStanding  = (allGrades.Count() - (reverse.IndexOf(grade)));
            
            if (frontStanding == backStanding)
            {
                return (frontStanding.ToString() + "/" + allGrades.Count());
            }

            return (frontStanding.ToString() + "-" + backStanding.ToString() + "/" + allGrades.Count());
        }
        
        /// <summary>
        /// Arranges all final grades
        /// in descending order.
        /// </summary>
        /// <returns>Returns a list of ordered final grades.</returns>
        public List<float> AllFinalGradesInOrder(int courseInstanceID)
        {
            List<float> allFinalGradesInOrder = new List<float>();

            var allPersons = (from p in _persons.All()
                              select p).ToList();

            foreach (var p in allPersons)
            {
                allFinalGradesInOrder.Add(GetCurrentFinalGrade(courseInstanceID, p.SSN));
            }

            allFinalGradesInOrder.Sort();
            allFinalGradesInOrder.Reverse();

            return allFinalGradesInOrder;
        }

        /// <summary>
        /// Gets a students final grade ranking
        /// among others in a given course. If you have the 
        /// same grade as others you wont get a precise ranking
        /// </summary>
        /// <param name="studentID"></param>
        /// <returns>Returns the final rankings of a student as a string.</returns>
        public String GetFinalRankings(int courseInstanceID, String studentID)
        {
            var allGrades = AllFinalGradesInOrder(courseInstanceID);
            var reverse = new List<float>(allGrades);
            reverse.Sort();

            var grade = GetCurrentFinalGrade(courseInstanceID, studentID);

            int frontStanding = ((allGrades.IndexOf(grade)) + 1);
            int backStanding = (allGrades.Count() - (reverse.IndexOf(grade)));

            if (frontStanding == backStanding)
            {
                return (frontStanding.ToString() + "/" + allGrades.Count());
            }

            return (frontStanding.ToString() + "-" + backStanding.ToString() + "/" + allGrades.Count());
        }

        /// <summary>
        /// Gets a students final grade in the given course
        /// </summary>
        /// <param name="courseInstanceID"></param>
        /// <param name="studentID"></param>
        /// <returns>Returns the final grade of student as a string.</returns>
        public String GetFinalGrade(int courseInstanceID, String studentID)
        {
            String passed = "";

            var checkIfFailed = (from p in _projects.All()
                                         where p.CourseInstanceID == courseInstanceID
                                               && p.MinGradeToPassCourse != null
                                         select p).ToList();
            foreach(var c in checkIfFailed)
            {
                if(c.MinGradeToPassCourse > GetGrade(c.ID, studentID))
                {
                    return "Failed to pass exam";
                }
            }

            float finalGrade = GetCurrentFinalGrade(courseInstanceID, studentID);
            float totalPossibleGrade = ((from p in _projects.All()
                                      where p.CourseInstanceID == courseInstanceID
                                            && p.ProjectGroupID == null
                                       select p.Weight).ToList()).Sum();
            
            var groupGrade = (from p in _projects.All()
                         where p.CourseInstanceID == courseInstanceID
                               && p.ProjectGroupID != null
                         select p).ToList();

            float previous = -1;

            foreach (var p in groupGrade)
            {
                if (p.ProjectGroupID == previous)
                {
                    continue;
                }
                var count = (from pg in _projectGroups.All()
                         where pg.ID == p.ProjectGroupID
                         select pg.GradedProjectsCount).FirstOrDefault();

                totalPossibleGrade += p.Weight * count;
                previous = p.ProjectGroupID.GetValueOrDefault();
            }

            float factor = ((float)100 / totalPossibleGrade);
            finalGrade = finalGrade * factor;
            finalGrade = finalGrade * 2;
            var fin = (Math.Round(System.Convert.ToDouble(finalGrade), MidpointRounding.AwayFromZero)) / 2;

            if (fin >= 5)
            {
                passed = "Passed";
            }
            else
            {
                passed = "Failed";
            }

            if (fin > 10)
            {
                fin = 10;
            }
            return ( fin.ToString()+ " - " + passed);
        }

        /// <summary>
        /// Returns all grades for a single student
        /// </summary>
        /// <param name="studentID"></param>
        /// <returns>Returns a list of all grades for a given student.</returns>
        public List<float> GetAllGradesByStudent(String studentID)
        {
            var allGrades = (from g in _grades.All()
                            where g.PersonID == studentID
                            select g.StudentGrade).ToList();

            return allGrades;
        }

        /// <summary>
        /// Returns a list of all students and there grade for the given project
        /// </summary>
        /// <param name="courseInstanceID"></param>
        /// <param name="projectID"></param>
        /// <returns>Returns a list of projects.</returns>
        public List<String> GetProjectOverView(int projectID)
        {
            var allGrades = (from g in _grades.All()
                        join per in _persons.All() on g.PersonID equals per.SSN
                        where g.ProjectID == projectID
                        select per.Name.ToString()+ " - " + g.StudentGrade.ToString()).ToList();

            return allGrades;
        }

        /// <summary>
        /// Returns a list with all student names and there grade in a current course
        /// </summary>
        /// <param name="courseInstanceID"></param>
        /// <returns>Returns a list of all final grades.</returns>
        public List<String> GetFinalGradeOverView(int courseInstanceID)
        {
            List<String> allGrades = new List<String>();
            var allPersons = (from p in _persons.All()
                              select p).ToList();
            foreach(var p in allPersons)
            {
                String temp = GetFinalGrade(courseInstanceID, p.SSN);
                allGrades.Add(p.Name + " - " + temp);
            }

            return allGrades;
        }
    }
}
