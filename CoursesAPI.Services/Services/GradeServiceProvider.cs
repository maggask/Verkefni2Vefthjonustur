using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns></returns>
        public Project AddProject(ProjectCreateViewModel model)
        {
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
        /// <returns></returns>
        public Grade AddGrade(GradeCreateViewModel model)
        {
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
        /// <returns></returns>
        public ProjectGroup AddProjectGroup(ProjectGroupCreateViewModel model)
        {
            var pg = new ProjectGroup
            {
                ID = model.ID,
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
        /// <returns></returns>
        public float GetGroupGrade(int ID)
        {
            var theGroup = (from h in _projectGroups.All()
                           where (h.ID == ID)
                           select h).SingleOrDefault();

            int howMany = theGroup.GradedProjectsCount;

            var allProjects = (from g in _grades.All()
                               join p in _projects.All() on g.ProjectID equals p.ID 
                               orderby g.StudentGrade descending
                               where p.ProjectGroupID == ID
                               select g).Take(howMany).ToList();

            var weight = (from p in _projects.All()
                          where p.ProjectGroupID == ID
                          select p.Weight).FirstOrDefault();

            return (allProjects.Average(x => x.WeightedStudentGrade));
        }

        /// <summary>
        /// Gets current final grade of course
        /// for a given student.
        /// </summary>
        /// <param name="studentID"></param>
        /// <returns></returns>
        public float GetFinalGrade(String studentID)
        {
            float finalGrade = 0;

            var allBasicGrades = (from g in _grades.All()
                             join p in _projects.All() on g.ProjectID equals p.ID 
                             where (g.PersonID == studentID 
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

                finalGrade += GetGroupGrade(p.ProjectGroupID.GetValueOrDefault());
                previous = p.ProjectGroupID.GetValueOrDefault();
            }

            var allHigherThanProjects = (from g in _grades.All()
                                        join p in _projects.All() on g.ProjectID equals p.ID
                                        where (g.PersonID == studentID
                                                && p.ProjectGroupID == null
                                                && p.OnlyHigherThanProjectID != null)
                                        select p).ToList();

            foreach (var proj in allHigherThanProjects)
            {
                var grade1 = (from g in _grades.All()
                              where g.ProjectID == proj.ID
                              select g.WeightedStudentGrade).SingleOrDefault();

                var grade2 = (from g in _grades.All()
                              where g.ProjectID == proj.OnlyHigherThanProjectID
                              select g.WeightedStudentGrade).SingleOrDefault();

                if (grade2 > grade1)
                {
                    finalGrade += (grade2 - grade1);
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
        /// <returns></returns>
        public float GetGrade(int projectID, String studentID)
        {
            var grade = (from g in _grades.All()
                         where g.ProjectID == projectID
                                && g.PersonID == studentID
                         select g.StudentGrade).SingleOrDefault();

            return grade;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public List<float> AllGradesInOrder(int projectID)
        {
            var allGrades = (from g in _grades.All()
                             where g.ProjectID == projectID
                             orderby g.StudentGrade descending
                             select g.StudentGrade).ToList();
            return allGrades;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public String GetProjectRankings(String studentID, int projectID)
        {
            var allGrades = AllGradesInOrder(projectID);
            var grade = GetGrade(projectID, studentID);
            int standing = allGrades.BinarySearch(grade);
            String rankings = (standing.ToString() + "/" + allGrades.Count());
            return rankings;
        }
        
        /**public List<float> AllFinalGradesInOrder()
        {
            var allStudents = (from p in _persons.All()
                                   where p.)
            foreach (var t in _persons)
            {

            }
        }

        public String GetFinalRankings(String studentID, int projectID)
        {

        }*/
    }
}
